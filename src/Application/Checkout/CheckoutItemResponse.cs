namespace Application.Checkout;

public sealed record CheckoutItemResponse(
    decimal UnitPrice,
    decimal TotalAmount,
    bool IsBundleDiscounted,
    decimal BundleQuantity);
