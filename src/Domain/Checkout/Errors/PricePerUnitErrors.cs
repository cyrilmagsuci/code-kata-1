using SharedKernel;

namespace Domain.Checkout.Errors;

public static class PricePerUnitErrors
{
    public static readonly Error ZeroOrNegative = Error.Problem("PricePerUnit.ZeroOrNegative", "PricePerUnit is zero or negative");
}
