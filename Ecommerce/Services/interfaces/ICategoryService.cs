using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces;

public interface ICategoryService : IBaseService<Category>
{
    IQueryable<Category> GetCategoryByName(string name);
}