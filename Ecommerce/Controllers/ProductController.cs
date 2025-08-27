using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class ProductController : GenericController<Product>
{
    private new readonly IProductService _service;

    public ProductController(IProductService service) : base(service)
    {
        _service = service;
    }

    //Price Filters
    [HttpGet("under-price/{maxPrice}")]
    public IActionResult GetUnderPrice(int maxPrice)
    {
        var products = _service.GetUnderPrice(maxPrice);
        var result = products.ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("above-price/{minPrice}")]
    public IActionResult GetAbovePrice(int minPrice)
    {
        var products = _service.GetAbovePrice(minPrice);
        var result = products.ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    //Searches
    [HttpGet("by-category/{categoryId}")]
    public IActionResult GetProductBYCategory(int categoryId)
    {
        var products = _service.GetProductByCategory(categoryId);
        var result = products.ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("by-brand/{brandId}")]
    public IActionResult GetProductByBrand(int brandId)
    {
        var products = _service.GetProductByBrand(brandId);
        var result = products.ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("by-name/{name}")]
    public IActionResult GetProductByName(string name)
    {
        var products = _service.GetProductByName(name);
        var result = products.ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    //Stock Processes
    [HttpGet("check-stock/{productId}")]
    public async Task<IActionResult> CheckProductStock(int productId)
    {
        var stock = await _service.CheckProductStock(productId);
        return stock != null ? Ok(stock) : NotFound();
    }

    [HttpPut("update-stock/{productId}/{newStock}")]
    public async Task<IActionResult> UpdateStock(int productId, int newStock)
    {
        var product = await _service.UpdateStock(productId, newStock);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPatch("increase-stock/{productId}/{amountToIncrease}")]
    public async Task<IActionResult> IncreaseStock(int productId, int amountToIncrease)
    {
        var product = await _service.IncreaseStock(productId, amountToIncrease);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPatch("decrease-stock/{productId}/{amountToDecrease}")]
    public async Task<IActionResult> DecreaseStock(int productId, int amountToDecrease)
    {
        var product = await _service.DecreaseStock(productId, amountToDecrease);
        return product != null ? Ok(product) : NotFound();
    }

}