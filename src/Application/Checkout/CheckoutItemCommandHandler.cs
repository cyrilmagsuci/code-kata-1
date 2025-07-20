using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Specifications;
using Domain.Checkout;
using Domain.Checkout.PropertyTypes;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Checkout;

internal sealed class CheckoutItemCommandHandler(
    IApplicationDbContext applicationDbContext,
    IUnitOfWork unitOfWork) : ICommandHandler<CheckoutItemCommand, Amount>
{
    public async Task<Result<Amount>> Handle(CheckoutItemCommand command, CancellationToken cancellationToken)
    {
    
        PromoCode[] promoCodes = command.PromoCodes.ToArray();
        string[] promoCodesFilter = promoCodes.Select(x => x.Value).ToArray();
        IReadOnlyList<Domain.Checkout.PricingRule> pricingRules =
            await applicationDbContext.PricingRules
                .WithSpecification(
                    new GetPricingRuleSpecification(rule => promoCodesFilter.Contains(rule.PromoCode.Value)))
                .ToListAsync(cancellationToken);

        var getPricingRuleSpecification = new GetCheckoutSpecification(x => x.UserSessionId == command.UserSessionId);

        Domain.Checkout.Checkout? checkout =
            await applicationDbContext.Checkouts.WithSpecification(getPricingRuleSpecification)
                .FirstOrDefaultAsync(cancellationToken);

        bool newSession = false;
        if (checkout is null)
        {
            checkout = Domain.Checkout.Checkout.Create(command.UserSessionId, promoCodes, pricingRules);
            newSession = true;
        }

        checkout.SetPricingRules(pricingRules);
        foreach (CheckoutItem existingCheckoutItem in checkout.CheckoutItems)
        {
            checkout.Scan(existingCheckoutItem);
        }

        var checkoutItem = CheckoutItem.Create(
            checkout.Id,
            command.Sku,
            Quantity.From(command.Quantity.Value),
            command.UnitOfMeasure);
        checkout.Scan(checkoutItem);

        if (newSession)
        {
            applicationDbContext.AddEntity(checkout);
        }
        else
        {
            applicationDbContext.UpdateEntity(checkout);
        }
        applicationDbContext.AddEntity(checkoutItem);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Amount.From(checkout.Total);
    }
}
