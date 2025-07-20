using Domain.Checkout;
using Domain.Checkout.PropertyTypes;
using Domain.UnitTests;
using FluentAssertions;

namespace Application.UnitTests.CheckoutItems;

public class CheckoutTests
{
    /* similar to : 
     * def test_totals
         assert_equal(  0, price(""))
         assert_equal( 50, price("A"))
         assert_equal( 80, price("AB"))
         assert_equal(115, price("CDBA"))

         assert_equal(100, price("AA"))
         assert_equal(130, price("AAA"))
         assert_equal(180, price("AAAA"))
         assert_equal(230, price("AAAAA"))
         assert_equal(260, price("AAAAAA"))

         assert_equal(160, price("AAAB"))
         assert_equal(175, price("AAABB"))
         assert_equal(190, price("AAABBD"))
         assert_equal(190, price("DABABA"))
       end
     */
    [Theory]
    [InlineData(0, "")]
    [InlineData(50, "A")]
    [InlineData(80, "AB")]
    [InlineData(115, "CDBA")]

    [InlineData(100, "AA")]
    [InlineData(130, "AAA")]
    [InlineData(180, "AAAA")]
    [InlineData(230, "AAAAA")]
    [InlineData(260, "AAAAAA")]
    public void ReturnSuccess_WhenCreateSucceeds(decimal expectedTotal, string skus)
    {
        var promoCode = PromoCode.From("default");
        List<Domain.Checkout.PricingRule> pricingRules = PricingRulesTestData.GetPricingRules(promoCode);

        var userSessionId = Guid.NewGuid();
        var checkout = Domain.Checkout.Checkout.Create(userSessionId,[promoCode], pricingRules);
        var unitOfMeasure = UnitOfMeasure.From("piece");

        foreach (char sku in skus)
        {
            checkout.Scan(CheckoutItem.Create(checkout.Id, Sku.From(sku.ToString()), Quantity.Create(1).Value, unitOfMeasure));
        }
        
        checkout.Total.Should().Be(expectedTotal);
    }

    /* similar to:
       def test_incremental
         co = CheckOut.new(RULES)
         assert_equal(  0, co.total)
         co.scan("A");  assert_equal( 50, co.total)
         co.scan("B");  assert_equal( 80, co.total)
         co.scan("A");  assert_equal(130, co.total)
         co.scan("A");  assert_equal(160, co.total)
         co.scan("B");  assert_equal(175, co.total)
       end
     */
    [Fact]
    public void Handle_Should_ReturnSuccess_WhenCreateSucceeds()
    {
        var promoCode = PromoCode.From("default");
        List<Domain.Checkout.PricingRule> pricingRules = PricingRulesTestData.GetPricingRules(promoCode);

        var userSessionId = Guid.NewGuid();
        var checkout = Domain.Checkout.Checkout.Create(userSessionId,[promoCode], pricingRules);
        var unitOfMeasure = UnitOfMeasure.From("piece");
    
        checkout.Total.Should().Be(0);
        
        checkout.Scan(CheckoutItem.Create(checkout.Id,Sku.From("A"), Quantity.From(1), unitOfMeasure));
        checkout.Total.Should().Be(50);
        
        checkout.Scan(CheckoutItem.Create(checkout.Id,Sku.From("B"), Quantity.From(1), unitOfMeasure));
        checkout.Total.Should().Be(80);
        
        checkout.Scan(CheckoutItem.Create(checkout.Id,Sku.From("A"), Quantity.From(1), unitOfMeasure));
        checkout.Total.Should().Be(130);
        
        checkout.Scan(CheckoutItem.Create(checkout.Id,Sku.From("A"), Quantity.From(1), unitOfMeasure));
        checkout.Total.Should().Be(160);
        
        checkout.Scan(CheckoutItem.Create(checkout.Id,Sku.From("B"), Quantity.From(1), unitOfMeasure));
        checkout.Total.Should().Be(175);
    }

}
