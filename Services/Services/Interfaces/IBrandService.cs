using HugeProject.Models;

namespace HugeProject.Services.Interfaces;

public interface IBrandService : IBaseService<Brand>
{
    IQueryable<Brand>? GetBrandByName(string name);
} 