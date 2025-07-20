using Domain.Checkout.PropertyTypes;
using SharedKernel;

namespace Domain.Checkout;

public sealed class CheckoutItem : Entity
{
    private CheckoutItem(Guid id, Guid checkoutId, Sku sku, Quantity quantity, UnitOfMeasure unitOfMeasure)
        : base(id)
    {
        CheckoutId = checkoutId;
        Sku = sku;
        Quantity = quantity;
        UnitOfMeasure = unitOfMeasure;
    }

    private CheckoutItem()
    {
    }

    public Guid CheckoutId { get; private set; }
    public Sku Sku { get; private set; }

    public Quantity Quantity { get; private set; }

    public UnitOfMeasure UnitOfMeasure { get; private set; }

    public static CheckoutItem Create(Guid checkoutId, Sku sku, Quantity quantity, UnitOfMeasure unitOfMeasure)
    {
        var user = new CheckoutItem(Guid.NewGuid(),  checkoutId, sku, quantity, unitOfMeasure);

        return user;
    }
}
