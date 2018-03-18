using System;
using System.Linq.Expressions;

namespace EF.Experiments.Data.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}