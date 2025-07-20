using Domain.Checkout.PropertyTypes;

namespace Application.Checkout;

public sealed record CheckoutItemRequest(
    Guid UserSessionId,
    string Sku,
    decimal Quantity,
    string UnitOfMeasure,
    IReadOnlyList<string> PromoCodes);
