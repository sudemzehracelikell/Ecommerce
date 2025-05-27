using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.models;

namespace ECommerce.Controllers;
using ECommerce.Models;

[ApiController]

[Route("api/categoryController")]
public class CategoryController : ControllerBase  
{

    private readonly IGenericRepository<Category> _repository; 

    public CategoryController(IGenericRepository<Category> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetAllCategory()
    {
        var category = await _repository.GetAll();
        return category;
    }
    [HttpGet("{id}")]
    public async Task < Category >GetByIdCategory(int id)
    {
        var category = await _repository.GetById(id);
        if (category != null)
            return category;
        return null;
    }

    [HttpPost]
    public async Task CreateCategory(Category newProduct)
    {
        await _repository.Create(new Category());
    }

    [HttpDelete("{id}")]
    public async Task DeleteCategory(int id)
    {
        await _repository.Delete(id);
    }
    [HttpPut]
    public async Task UpdateBrand(Category newCategory)
    {
        await _repository.Update(newCategory);
    }

}