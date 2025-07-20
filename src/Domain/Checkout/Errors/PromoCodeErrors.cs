using SharedKernel;

namespace Domain.Checkout.Errors;

public static class PromoCodeErrors
{
    public static readonly Error Empty = Error.Problem("PromoCode.Empty", "PromoCode is empty");
}
