using Application.Abstractions.Data;
using Application.Checkout;
using Domain.Checkout;
using Domain.Checkout.PropertyTypes;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Application.UnitTests.CheckoutItems;

public class CheckoutItemCommandTests
{
    private readonly CheckoutItemCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IApplicationDbContext _applicationDbContext;

    public CheckoutItemCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _applicationDbContext = Substitute.For<IApplicationDbContext>();

        _handler = new CheckoutItemCommandHandler(_applicationDbContext, _unitOfWorkMock);
    }

    /*
    similar to:
         def test_totals
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
    public async Task Handle_Should_ReturnSuccess(decimal expectedTotal, string skus)
    {
        // Arrange
        var userSessionId = Guid.NewGuid();
        var userCheckoutItems = new Dictionary<Guid, List<CheckoutItem>>();

        PromoCode promoCode = PromoCode.Create("default").Value;
        List<Domain.Checkout.PricingRule> pricingRules = PricingRulesTestData.GetPricingRules(promoCode);

        var pricingRuleDbSet = pricingRules.MockDbSet();
        _applicationDbContext.PricingRules.Returns(pricingRuleDbSet);

        var userCheckouts = new List<Domain.Checkout.Checkout>();
        var checkoutDbSet = userCheckouts.MockDbSet();
        _applicationDbContext.Checkouts.Returns(checkoutDbSet);

        _applicationDbContext.When(x => x.AddEntity(Arg.Any<CheckoutItem>()))
            .Do(callInfo =>
            {
                CheckoutItem? checkoutItem = callInfo.Arg<CheckoutItem>();
                List<CheckoutItem> checkoutItems =
                    userCheckoutItems.TryGetValue(userSessionId, out List<CheckoutItem>? item)
                        ? item
                        : new List<CheckoutItem>();
                checkoutItems.Add(checkoutItem);
                userCheckoutItems[userSessionId] = checkoutItems;
            });

        _applicationDbContext.When(x => x.AddEntity(Arg.Any<Domain.Checkout.Checkout>()))
            .Do(callInfo =>
            {
                var checkout = callInfo.Arg<Domain.Checkout.Checkout>();
                userCheckouts.Add(checkout);
            });

        // Act
        decimal total = 0;
        foreach (char sku in skus)
        {
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Create("piece").Value;
            CheckoutItemCommand command = new(
                userSessionId,
                Sku.Create(sku.ToString()).Value,
                Quantity.Create(1).Value,
                unitOfMeasure, [promoCode]);

            Result<CheckoutItemResponse> result = await _handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();

            total = result.Value.TotalAmount;
        }

        // Assert
        total.Should().Be(expectedTotal);
    }
}
