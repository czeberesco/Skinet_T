using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteriaExpression) : ISpecification<T>
{
    protected BaseSpecification() : this(null)
    {
    }

    public Expression<Func<T, bool>>? CriteriaExpression => criteriaExpression;

    public Expression<Func<T, object>>? OrderByExpression { get; private set; }

    public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (criteriaExpression != null)
        {
            query = query.Where(criteriaExpression);
        }

        return query;
    }

    protected void AddOrderByExpression(Expression<Func<T, object>> orderBy)
    {
        OrderByExpression = orderBy;
    }

    protected void AddOrderByDescendingExpression(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescendingExpression = orderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyPaging(int skip, int take)
    {
        IsPagingEnabled = true;
        Skip = skip;
        Take = take;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteriaExpression) :
    BaseSpecification<T>(criteriaExpression), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null)
    {
    }

    public Expression<Func<T, TResult>>? SelectExpression { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        SelectExpression = selectExpression;
    }
}