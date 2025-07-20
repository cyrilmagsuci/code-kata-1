using System.Linq.Expressions;
using SharedKernel;

namespace Application.Specifications;

public abstract class Specification<TEntity>
    where TEntity : Entity
{
    private Func<IQueryable<TEntity>, IQueryable<TEntity>>? _queryActions;

    protected Specification(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    protected void AddQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryAction)
    {
        Ensure.NotNull(queryAction);
        _queryActions = queryAction;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

    public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
    {
        if (Criteria is not null)
        {
            query = query.Where(Criteria);
        }

        if (_queryActions is not null)
        {
            query = _queryActions(query);
        }

        return query;
    }
}
