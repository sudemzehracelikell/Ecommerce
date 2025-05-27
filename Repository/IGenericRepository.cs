namespace ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;

public interface IGenericRepository<TEntitiy> where TEntitiy : class
{
    Task<IEnumerable<TEntitiy>> GetAll(); //The key used to read a list in the database will change to read-only.  icollection
    Task <TEntitiy> GetById(int id);
    Task  Create(TEntitiy newEntitiy); //asycn
    Task Update(TEntitiy newEntity);
    Task Delete(int id);
    Task SaveChanges();
}