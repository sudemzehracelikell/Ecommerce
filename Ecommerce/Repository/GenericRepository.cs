

using System.Linq.Expressions;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository;

public class GenericRepository<TEntity> : IQueryableRepository<TEntity>, IEnumarableRepository<TEntity> where TEntity : class
{

    private readonly Context _context;
    private readonly DbSet<TEntity> table; // !!!!!!!

    public GenericRepository(Context context)
    {
        _context = context;
        table = _context.Set<TEntity>();
    }

    //Ienumarable methods
    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        var entities = await table.ToListAsync();
        return entities;
    }

    public async Task<TEntity> GetById(int id)
    {
        var entity = await table.FindAsync(id);
        return entity;
    }

    public async Task Create(TEntity newEntitiy)
    {
        await table.AddAsync(newEntitiy);
        await SaveChange();
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);
        table.Remove(entity);
        await SaveChange();
    }

    public async Task Update(TEntity newEntity)
    {
        table.Update(newEntity);
        await SaveChange();
    }


    //IQueryable methods
    public IQueryable<TEntity> GetAllQueryable()
    {
        return table.AsQueryable();
    }

    public IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> predicate)
    {
        return table.Where(predicate);
    }

    public IQueryable<TEntity> GetWithIncludes(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = table;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

}