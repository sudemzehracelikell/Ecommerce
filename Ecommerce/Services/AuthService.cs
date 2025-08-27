using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;  

        public AuthService(IOptions<JwtSettings> jwtSettings) 
        {
            _jwtSettings = jwtSettings.Value; 
        }
        

        public string CreateToken(User user) 
        {
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Name, user.Name ?? ""), 
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("Email", user.EMail ?? "") 
                
            };
                        
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken( //token’ı :
                issuer: _jwtSettings.Issuer, //kim üretti
                audience: _jwtSettings.Audience, // kim kullanacak
                claims: claims, 
                expires: now.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256 
                )
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}