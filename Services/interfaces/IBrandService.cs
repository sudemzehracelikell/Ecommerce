using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces;

public interface IBrandService : IBaseService<Brand>
{
    IQueryable<Brand>? GetBrandByName(string name);
} 