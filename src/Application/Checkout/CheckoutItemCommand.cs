using Application.Abstractions.Messaging;
using Domain.Checkout.PropertyTypes;

namespace Application.Checkout;

public sealed record CheckoutItemCommand(
    Guid UserSessionId,
    Sku Sku,
    Quantity Quantity,
    UnitOfMeasure UnitOfMeasure,
    IReadOnlyList<PromoCode> PromoCodes)
    : ICommand<Amount>;
