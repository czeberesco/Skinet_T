using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? CriteriaExpression { get; }
    Expression<Func<T, object>>? OrderByExpression { get; }
    Expression<Func<T, object>>? OrderByDescendingExpression { get; }

    bool IsDistinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? SelectExpression { get; }
}