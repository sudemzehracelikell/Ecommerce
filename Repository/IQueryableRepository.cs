
using System.Linq.Expressions;

namespace Ecommerce.Repository;

public interface IQueryableRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAllQueryable();
    IQueryable<TEntity> FilterBy(Expression<Func<TEntity,bool>> predicate);
    IQueryable<TEntity> GetWithIncludes(params Expression<Func<TEntity, object>>[] includes);
}