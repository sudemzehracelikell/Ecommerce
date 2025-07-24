using Ecommerce.Models;
using Ecommerce.Repository;

namespace Ecommerce.Services;

public class BrandService : BaseService<Brand>, Interfaces.IBrandService
{
    public BrandService(IEnumarableRepository<Brand> _enumRepository, IQueryableRepository<Brand> _queryRepository)
    : base(_enumRepository, _queryRepository)
    { }

    public IQueryable<Brand>? GetBrandByName(string name)
    {
        var b =_queryRepository.FilterBy(b => b.Name == name);
        return b ?? null;
    }
    
}