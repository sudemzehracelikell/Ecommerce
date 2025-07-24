using System.Linq.Expressions;

namespace Ecommerce.Services.Interfaces;

public interface IBaseService<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Create(TEntity newEntity);
    Task Update(TEntity newEntity);
    Task Delete(int id);

    IQueryable<TEntity> GetAllQueryable();
    IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetWithIncludes(params Expression<Func<TEntity, object>>[] includes);
    
}   