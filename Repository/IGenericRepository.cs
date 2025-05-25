namespace ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;

public interface IGenericRepository<TEntitiy> where TEntitiy : class
{
    IEnumerable<TEntitiy> GetAll();
    TEntitiy GetById(int id);
    void Create(TEntitiy newEntitiy);
    void Update(TEntitiy newEntity);
    void Delete(int id);
}