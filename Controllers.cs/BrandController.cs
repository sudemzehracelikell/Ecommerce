using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.models;

namespace ECommerce.Controllers;
using ECommerce.Models;

[ApiController]

[Route("api/brandController")]
public class BrandController : ControllerBase  
{

    private readonly IGenericRepository<Brand> _repository; 

    public BrandController(IGenericRepository<Brand> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Brand>> GetAllBrand()
    {
        var brand = await _repository.GetAll();
        return brand;
    }
    [HttpGet("{id}")]
    public async Task < Brand >GetByIdBrand(int id)
    {
        var brand = await _repository.GetById(id);
        if (brand != null)
            return brand;
        return null;
    }

    [HttpPost]
    public async Task CreateBrand(Brand newBrand)
    {
        await _repository.Create(newBrand);
    }

    [HttpDelete("{id}")]
    public async Task DeleteBrand(int id)
    {
        await _repository.Delete(id);
    }
    [HttpPut]
    public async Task UpdateBrand(Brand newBrand)
    {
        await _repository.Update(newBrand);
    }

}