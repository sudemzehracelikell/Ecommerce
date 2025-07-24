using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : GenericController<Category>
{
    private new readonly ICategoryService _service;

    public CategoryController(ICategoryService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("by-name/{name}")]
    public IActionResult GetCategoryByName(string name)
    {
        var categories = _service.GetCategoryByName(name);
        return categories != null ? Ok(categories.ToList()) : NotFound();
    }
}