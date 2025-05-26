using ECommerce.Models;
using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using WebApp1.models;

namespace ECommerce.Controllers;
using ECommerce.Models;

[ApiController]

[Route("api/productController")]
public class ProductController : ControllerBase
{
    private readonly IGenericRepository<Product> _repository;

    public ProductController(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<Product> GetAllProducts()
    {
        var products = _repository.GetAll();
        return products;
    }
    [HttpGet("{id}")]
    public Product GetByIdProduct(int id)
    {
        var product = _repository.GetById(id);
        if (product != null)
            return product;
        return null;
    }

    [HttpPost]
    public void CreateProduct(Product newProduct)
    {
        _repository.Create(newProduct);
    }

    [HttpDelete("{id}")]
    public void DeleteProduct(int id)
    {
        _repository.Delete(id);
    }
    [HttpPut]
    public void UpdateProduct(Product newProduct)
    {
        _repository.Update(newProduct);
    }

}