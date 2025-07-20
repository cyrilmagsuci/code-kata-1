using System.Linq.Expressions;

namespace Application.Specifications;

public class GetPricingRuleSpecification : Specification<Domain.Checkout.PricingRule>
{
    public GetPricingRuleSpecification(Expression<Func<Domain.Checkout.PricingRule, bool>>? criteria = null) : base(criteria)
    {
    }
}
