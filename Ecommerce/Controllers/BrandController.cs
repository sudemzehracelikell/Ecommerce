using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BrandController : GenericController<Brand>
{ 
    private new readonly IBrandService _service;
    
    public BrandController(IBrandService service) : base(service)
    {
        _service = service;
    }
    
    [HttpGet("by-name/{name}")]
    public IActionResult GetBrandByName(string name)
    {
        var brands = _service.GetBrandByName(name);
        return brands != null ? Ok(brands.ToList()) : NotFound();
    }

}