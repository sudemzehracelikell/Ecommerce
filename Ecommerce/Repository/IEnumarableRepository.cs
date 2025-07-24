
namespace Ecommerce.Repository;

public interface IEnumarableRepository<TEntity> where TEntity : class
{   
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Create(TEntity newEntitiy);
    Task Update(TEntity newEntity);
    Task Delete(int id);
    Task SaveChange();
}