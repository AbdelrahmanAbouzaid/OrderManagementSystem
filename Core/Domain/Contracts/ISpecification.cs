

using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>>? Criteria { get; set; }

        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }
        Expression<Func<TEntity, object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
    }
}
