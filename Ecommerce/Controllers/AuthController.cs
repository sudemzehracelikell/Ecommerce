using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService; //JWT token üretmek için servis

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User request)
        {
            
            if (_userService.GetUserByName(request.Name!)?.Any() == true)
                
                return BadRequest("This username is already taken.");

            // Parola hashleme
            request.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash); //hashleme algoritması

            
            _userService.Create(request); // Kullanıcıyı veri tabanına kaydet

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _userService.GetUserByName(login.Name ?? "").FirstOrDefault();
            if (user == null)
            {
                return BadRequest("There is not user");
            }
            
            if (user.Name != login.Name)
            {
                return BadRequest("User not found");
            }
            
            if (!BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
            {
                return BadRequest("Password is wrong");
            }
            
            
            string token = _authService.CreateToken(user); // JWT token üret
            return Ok(new { token }); // Token’ı JSON olarak döndür    ??
        }
        [Authorize]
        [HttpGet]
        public IActionResult AuthanticatedOnlyEndpoint()
        {
            return Ok("You are Authanticated");
        }
        
        [Authorize(Roles = "Admin, User, Company")]
        [HttpGet]
        public IActionResult AdminEndpoint()
        {
            return Ok("You are Authanticated");
        }
    }
    
}