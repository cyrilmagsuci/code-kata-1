using SharedKernel;

namespace Domain.Checkout.Errors;

public static class AmountErrors
{
    public static readonly Error ZeroOrNegative = Error.Problem("Amount.ZeroOrNegative", "Amount is zero or negative");
}
