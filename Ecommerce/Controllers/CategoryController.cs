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
        var query = _service.GetCategoryByName(name);
        var categories = query.ToList();
        
        return categories is {Count :> 0} ? Ok(categories.ToList()) : NotFound();
    }
}