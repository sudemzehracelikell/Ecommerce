using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repository;

namespace Ecommerce.Services;

public class BrandService : BaseService<Brand>, Interfaces.IBrandService
{
    private readonly IQueryableRepository<Brand> _queryRepository;

    public BrandService(
        IEnumarableRepository<Brand> enumRepository,
        IQueryableRepository<Brand> queryRepository)
        : base(enumRepository, queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public IQueryable<Brand>? GetBrandByName(string name)
    {
        return _queryRepository.FilterBy(b => b.Name == name);
    }

    public Brand? GetById(int id)
    {
        return _queryRepository.FilterBy(b => b.Id == id).FirstOrDefault();
    }
}