using System.Linq.Expressions;
using Ecommerce.Models;
using Ecommerce.Repository;

namespace Ecommerce.Services;

public class BaseService<TEntity> : Interfaces.IBaseService<TEntity> where TEntity : BaseEntity
{
    protected readonly IEnumarableRepository<TEntity> _enumRepository;
    protected readonly IQueryableRepository<TEntity> _queryRepository;
    
    public BaseService(IEnumarableRepository<TEntity> enumRepository,
                        IQueryableRepository<TEntity> queryRepository)
    {
        _enumRepository = enumRepository;
        _queryRepository = queryRepository;
    }

    //IEnumarable
    public virtual async Task<IEnumerable<TEntity>> GetAll()
    => await _enumRepository.GetAll();

    public virtual async Task<TEntity> GetById(int id)
    => await _enumRepository.GetById(id);

    public virtual async Task Create(TEntity newEntity)
    => await _enumRepository.Create(newEntity);

    public virtual async Task Update(TEntity newEntity)
    => await _enumRepository.Update(newEntity);

    public virtual async Task Delete(int id)
    => await _enumRepository.Delete(id);


    //Queryable
    public virtual IQueryable<TEntity> GetAllQueryable()
    => _queryRepository.GetAllQueryable();

    public virtual IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> predicate)
    => _queryRepository.FilterBy(predicate);

    public virtual IQueryable<TEntity> GetWithIncludes(params Expression<Func<TEntity, object>>[] includes)
    => _queryRepository.GetWithIncludes(includes);
}