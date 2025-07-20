using Domain.Checkout.PropertyTypes;
using SharedKernel;

namespace Domain.Checkout;

public class PricingRule : Entity
{
    public PricingRule(PromoCode promoCode, Sku sku, PricePerUnit pricePerUnit, Quantity quantity
        , UnitOfMeasure unitOfMeasure, bool isBundle)
    {
        PromoCode = promoCode;
        Sku = sku;
        PricePerUnit = pricePerUnit;
        Quantity = quantity;
        IsBundle = isBundle;
        UnitOfMeasure = unitOfMeasure;
    }

    private PricingRule()
    {
    }

    public PromoCode PromoCode { get; private set; }
    public Sku Sku { get; private set; }
    public bool IsBundle { get; private set; }
    public Quantity Quantity { get; private set; }
    public PricePerUnit PricePerUnit { get; private set; }
    public UnitOfMeasure UnitOfMeasure { get; private set; }
}
