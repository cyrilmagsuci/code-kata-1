using Domain.Checkout.Errors;
using SharedKernel;

namespace Domain.Checkout.PropertyTypes;

public sealed record Quantity : PropertyType<Quantity, decimal>
{
    protected override Result<decimal> Validate()
    {
        return Value <= 0 ? Result.Failure<decimal>(QuantityErrors.ZeroOrNegative) : Result.Success(Value);
    }
}

