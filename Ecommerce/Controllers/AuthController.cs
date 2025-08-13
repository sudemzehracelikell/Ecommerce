using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService; //JWT token üretmek için servis

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User request)
        {
            
            if (_userService.GetUserByName(request.Name!)?.Any() == true)
                return BadRequest("This username is already taken.");

            // Parola hashleme
            request.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash); //hashleme algoritması

            request.State = true; // Varsayılan aktif kullanıcı
            _userService.Create(request); // Kullanıcıyı veri tabanına kaydet

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _userService.GetUserByName(login.Name ?? "").FirstOrDefault();
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _tokenService.GenerateToken(user); // JWT token üret
            return Ok(new { token }); // Token’ı JSON olarak döndür
        }
    }
}