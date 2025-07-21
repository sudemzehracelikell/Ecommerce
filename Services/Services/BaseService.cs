using System.Linq.Expressions;
using HugeProject.Repository;

namespace HugeProject.Services;

public class BaseService<TEntity> : Interfaces.IBaseService<TEntity> where TEntity : class
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
    public virtual Task<IEnumerable<TEntity>> GetAll()
    => _enumRepository.GetAll();

    public virtual Task<TEntity> GetById(int id)
    => _enumRepository.GetById(id);

    public virtual Task Create(TEntity newEntity)
    => _enumRepository.Create(newEntity);

    public virtual Task Update(TEntity newEntity)
    => _enumRepository.Update(newEntity);

    public virtual Task Delete(int id)
    => _enumRepository.Delete(id);


    //Queryable
    public virtual IQueryable<TEntity> GetAllQueryable()
    => _queryRepository.GetAllQueryable();

    public virtual IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> predicate)
    => _queryRepository.FilterBy(predicate);

    public virtual IQueryable<TEntity> GetWithIncludes(params Expression<Func<TEntity, object>>[] includes)
    => _queryRepository.GetWithIncludes(includes);
}