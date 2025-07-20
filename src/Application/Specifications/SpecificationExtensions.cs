using SharedKernel;

namespace Application.Specifications;

public static class SpecificationExtensions
{
    public static IQueryable<TEntity> WithSpecification<TEntity>(
        this IQueryable<TEntity> query,
        Specification<TEntity> specification)
    where TEntity : Entity
    {
        return specification.Apply(query);
    }
}
