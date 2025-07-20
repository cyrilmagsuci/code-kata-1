using Domain.Checkout.Errors;
using SharedKernel;

namespace Domain.Checkout.PropertyTypes;

public sealed record PricePerUnit: PropertyType<PricePerUnit, decimal>
{
    protected override Result<decimal> Validate()
    {
        return Value <= 0 ? Result.Failure<decimal>(PricePerUnitErrors.ZeroOrNegative) : Result.Success(Value);
    }
}
