using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public static class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        query = PreProcess(query, spec);

        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }
        
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;
    }

    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        query = PreProcess(query, spec);

        IQueryable<TResult>? selectQuery = query as IQueryable<TResult>;

        if (spec.SelectExpression != null)
        {
            selectQuery = query.Select(spec.SelectExpression);
        }

        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        if (spec.IsPagingEnabled)
        {
            selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
        }

        return selectQuery ?? query.Cast<TResult>();
    }

    private static IQueryable<TType> PreProcess<TType>(IQueryable<TType> query, ISpecification<TType> spec)
    {
        if (spec.CriteriaExpression != null)
        {
            query = query.Where(spec.CriteriaExpression);
        }

        if (spec.OrderByExpression != null)
        {
            query = query.OrderBy(spec.OrderByExpression);
        }

        if (spec.OrderByDescendingExpression != null)
        {
            query = query.OrderByDescending(spec.OrderByDescendingExpression);
        }

        return query;
    }
}