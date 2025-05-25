using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Repository;

public class GenericRepository<TEntitiy> : IGenericRepository<TEntitiy> where TEntitiy : class
{
    private readonly Context _context;

    public GenericRepository(Context context)
    {
        _context = context;
    }

    public IEnumerable<TEntitiy> GetAll()
    {
        var products = _context.Set<TEntitiy>().AsNoTracking();
        return products;
    }

    public TEntitiy GetById(int id)
    {
        var product = _context.Set<TEntitiy>().Find(id);
        return product;
    }

    public void Create(TEntitiy newEntitiy)
    {
        _context.Set<TEntitiy>().Add(newEntitiy);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var deletedProduct = GetById(id);
        _context.Set<TEntitiy>().Remove(deletedProduct);
        _context.SaveChanges();
    }

    public void Update(TEntitiy newEntity)
    {
        _context.Set<TEntitiy>().Update(newEntity);
        _context.SaveChanges();
    }

    public Task<IEnumerable<TEntitiy>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TEntitiy?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntitiy> CreateAsync(TEntitiy newEntity)
    {
        throw new NotImplementedException();
    }

    public TEntitiy UpdateAsync(TEntitiy entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}