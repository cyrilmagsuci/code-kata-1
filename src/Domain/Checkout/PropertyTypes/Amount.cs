using Domain.Checkout.Errors;
using SharedKernel;

namespace Domain.Checkout.PropertyTypes;

public sealed record Amount : PropertyType<Amount, decimal>
{
    protected override Result<decimal> Validate()
    {
        return Value <= 0 ? Result.Failure<decimal>(AmountErrors.ZeroOrNegative) : Result.Success(Value);
    }
}
