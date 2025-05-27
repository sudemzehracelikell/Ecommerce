using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.models;

namespace ECommerce.Controllers;
using ECommerce.Models;

[ApiController]

[Route("api/productController")]
public class ProductController : ControllerBase  //why?
{

    private readonly IGenericRepository<Product> _repository; // why use interface?

    public ProductController(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _repository.GetAll();
        return products;
    }
    [HttpGet("{id}")]
    public async Task < Product >GetByIdProduct(int id)
    {
        var product = await _repository.GetById(id);
        if (product != null)
            return product;
        return null;
    }

    [HttpPost]
    public async Task CreateProduct(Product newProduct)
    {
       await _repository.Create(newProduct);
    }

    [HttpDelete("{id}")]
    public async Task DeleteProduct(int id)
    {
        await _repository.Delete(id);
    }
    [HttpPut]
    public async Task UpdateProduct(Product newProduct)
    {
        await _repository.Update(newProduct);
    }

}