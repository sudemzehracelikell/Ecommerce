using Ecommerce.Models;
using Ecommerce.Repository;

namespace Ecommerce.Services;

public class CategoryService : BaseService<Category>, Interfaces.ICategoryService
{
    public CategoryService(IEnumarableRepository<Category> _enumRepository, IQueryableRepository<Category> _queryRepository)
    : base(_enumRepository, _queryRepository)
    { }

    public IQueryable<Category>? GetCategoryByName(string name)
    {
        var c = _queryRepository.FilterBy(c => c.Name == name);
        return c ?? null;
    }
    
}