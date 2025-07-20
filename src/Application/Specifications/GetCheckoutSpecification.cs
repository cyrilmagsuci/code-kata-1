using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications;

public class GetCheckoutSpecification : Specification<Domain.Checkout.Checkout>
{
    public GetCheckoutSpecification(Expression<Func<Domain.Checkout.Checkout, bool>>? criteria = null) : base(criteria)
    {
         AddQuery(query => query.Include(x => x.CheckoutItems));
    }
}
