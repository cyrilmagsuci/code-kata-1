using Application.Checkout;
using Application.IntegrationTests.Infrastructure;
using Domain.Checkout.PropertyTypes;
using FluentAssertions;
using SharedKernel;

namespace Application.IntegrationTests.Users;

public class CheckoutCommandTests : BaseIntegrationTest
{
    public CheckoutCommandTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Handle_Should_CreateUser_WhenCommandIsValid()
    {
        // Arrange
        var userSessionId = Guid.NewGuid();
        var command = new CheckoutItemCommand(userSessionId, Sku.From("A"), Quantity.From(1),
            UnitOfMeasure.From("piece"), [PromoCode.From("default")]);

        // Act
        Result<Amount> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(Amount.From(50));
    }
 
}
