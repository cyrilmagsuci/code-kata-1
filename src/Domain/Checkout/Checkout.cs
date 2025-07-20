using System.ComponentModel.DataAnnotations.Schema;
using Domain.Checkout.PropertyTypes;
using SharedKernel;

namespace Domain.Checkout;

public class Checkout : Entity
{
    private readonly List<CheckoutItem> _checkoutItems = new();
    private readonly Dictionary<CheckoutItemPriceKey, decimal> _calculatedItemPrices = new();

    private Checkout(Guid id, Guid userSessionId) : base(id)
    {
        UserSessionId = userSessionId;
    }

    public Guid UserSessionId { get; private set; }
    public List<CheckoutItem> CheckoutItems { get; private set; } = new();
    public List<PromoCode> PromoCodes { get; private set; } = new();

    [NotMapped] private IReadOnlyList<PricingRule> _pricingRules;

    public static Checkout Create(Guid userSessionId, IReadOnlyList<PromoCode> promoCodes,
        IReadOnlyList<PricingRule>? pricingRules = null)
    {
        return new Checkout(Guid.NewGuid(), userSessionId)
        {
            PromoCodes = new List<PromoCode>(promoCodes),
            _pricingRules = pricingRules ?? []
        };
    }

    public void SetPricingRules(IReadOnlyList<PricingRule>? pricingRules)
    {
        _pricingRules = pricingRules;
    }

    public void Scan(CheckoutItem item)
    {
        _checkoutItems.Add(item);

        decimal runningQuantity = GetRunningQuantity(item).Value;

        decimal total = 0;

        while (runningQuantity > 0)
        {
            PricingRule pricingRule =
                GetPricingRule(item.Sku, Quantity.From(runningQuantity), item.UnitOfMeasure);
            total += pricingRule.PricePerUnit.Value * (pricingRule.IsBundle ? 1 : pricingRule.Quantity.Value);
            runningQuantity -= pricingRule.Quantity.Value;
        }

        _calculatedItemPrices[new CheckoutItemPriceKey(item.Sku, item.UnitOfMeasure)] = total;
    }

    public Quantity GetRunningQuantity(CheckoutItem item)
    {
        decimal runningQuantity = _checkoutItems
            .Where(x => x.Sku == item.Sku && x.UnitOfMeasure == item.UnitOfMeasure)
            .Sum(x => x.Quantity.Value);

        return Quantity.From(runningQuantity);
    }

    public PricingRule GetPricingRule(Sku sku, Quantity quantity, UnitOfMeasure unitOfMeasure)
    {
        PricingRule applicablePricingRule = _pricingRules
            .Where(x => x.Sku == sku &&
                        x.UnitOfMeasure == unitOfMeasure &&
                        x.Quantity.Value <= quantity.Value)
            .OrderByDescending(x => x.Quantity.Value)
            .Take(1).Single();
        return applicablePricingRule;
    }

    public decimal Total => _calculatedItemPrices.Sum(x => x.Value);

    private record CheckoutItemPriceKey(Sku Sku, UnitOfMeasure UnitOfMeasure);
}
