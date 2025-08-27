
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : GenericController<User>
{
    
    private new readonly IUserService _service;
    private readonly IAuthService _authService;
    public UserController(IUserService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("by-name/{name}")]
    public IActionResult GetUserByName(string name)
    {
        var users = _service.GetUserByName(name);
        return users != null ? Ok(users.ToList()) : NotFound();
    }
    
}