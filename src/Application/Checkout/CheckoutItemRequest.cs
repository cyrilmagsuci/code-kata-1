using Domain.Checkout.PropertyTypes;

namespace Application.Checkout;

public sealed record CheckoutItemRequest(
    Guid UserSessionId,
    Sku Sku,
    Quantity Quantity,
    UnitOfMeasure UnitOfMeasure,
    IReadOnlyList<PromoCode> PromoCodes);
