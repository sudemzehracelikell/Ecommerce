using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Repository;

public class GenericRepository<TEntitiy> : IGenericRepository<TEntitiy> where TEntitiy : class
{
    private readonly Context _context;
    private readonly DbSet<TEntitiy> table;
    public GenericRepository(Context context)
    {
        _context = context; //dependecy injection
        table= _context.Set<TEntitiy>();
        
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntitiy>> GetAll()
    {
        var products = await table.ToListAsync();
        return products;
    }

    public async Task<TEntitiy >GetById(int id)
    {
        var product =await table.FindAsync(id) ;
        return product;
    }

    public  async Task Create(TEntitiy newEntitiy)
    {
        await table.AddAsync(newEntitiy);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        var deletedProduct = await GetById(id);
        table.Remove(deletedProduct);
        await SaveChanges();
    }


    public async Task Update(TEntitiy newEntity)
    {
        table.Update(newEntity);
        await SaveChanges();
    }
    
}  //await usage
