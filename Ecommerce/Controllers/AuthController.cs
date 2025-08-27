using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using ECommerce.Validators;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService; 

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel request)
        {
            if (_userService.GetUserByName(request.Email!)?.Any() == true)
                return BadRequest("This username is already taken.");

            var user = new User
            {
                Name = request.Name,
                EMail = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserType = UserType.User,
                State = true,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            
            _userService.Create(user); 

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
           var validator = new LoginValidator().Validate(model);
            if(!validator.IsValid)
                throw new Exception(string.Join(",",validator.Errors.Select(x => x.ErrorMessage)));

            var user = _userService.Authenticate(model.Email,  model.Password);

            string token = _authService.CreateToken(user); // JWT token üret
            return Ok(new { token }); // Token’ı JSON olarak döndür    ??
        }
    }
}