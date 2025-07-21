using HugeProject.Models;

namespace HugeProject.Services.Interfaces;

public interface ICategoryService : IBaseService<Category>
{
    IQueryable<Category>? GetCategoryByName(string name);
}