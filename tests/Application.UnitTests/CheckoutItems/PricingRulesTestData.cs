using Domain.Checkout;
using Domain.Checkout.PropertyTypes;

namespace Application.UnitTests.CheckoutItems;

public static class PricingRulesTestData
{
    public static List<Domain.Checkout.PricingRule> GetPricingRules(PromoCode? promoCode = null)
    {
        promoCode ??= PromoCode.Create("default").Value;
        UnitOfMeasure unitOfMeasure = UnitOfMeasure.Create("piece").Value;
        var pricingRules = new List<Domain.Checkout.PricingRule>()
        {
            new(
                promoCode,
                Sku.Create("A").Value,
                PricePerUnit.Create(50).Value,
                Quantity.Create(1).Value,
                unitOfMeasure,
                isBundle: false),

            new(
                promoCode,
                Sku.Create("A").Value,
                PricePerUnit.Create(130).Value,
                Quantity.Create(3).Value,
                unitOfMeasure,
                isBundle: true),

            new(
                promoCode,
                Sku.Create("B").Value,
                PricePerUnit.Create(30).Value,
                Quantity.Create(1).Value,
                unitOfMeasure,
                isBundle: false),

            new(
                promoCode,
                Sku.Create("B").Value,
                PricePerUnit.Create(45).Value,
                Quantity.Create(2).Value,
                unitOfMeasure,
                isBundle: true),

            new(
                promoCode,
                Sku.Create("C").Value,
                PricePerUnit.Create(20).Value,
                Quantity.Create(1).Value,
                unitOfMeasure,
                isBundle: false),

            new(
                promoCode,
                Sku.Create("D").Value,
                PricePerUnit.Create(15).Value,
                Quantity.Create(1).Value,
                unitOfMeasure,
                isBundle: false),
        };
        return pricingRules;
    }
}
