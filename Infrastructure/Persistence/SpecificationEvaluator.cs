

using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
            where TEntity : BaseEntity
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            query = spec.IncludeExpressions.Aggregate(query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
