using Application.Checkout;
using Domain.Checkout.PropertyTypes;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Checkout;

public class CheckoutItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("checkout", async (
                CheckoutItemRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new CheckoutItemCommand(
                    request.UserSessionId,
                    request.Sku,
                    request.Quantity,
                    request.UnitOfMeasure,
                    request.PromoCodes);

                Result<Amount> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.CheckoutItems);
    }
}
