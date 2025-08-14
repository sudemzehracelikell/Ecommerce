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
        private readonly JwtSettings _jwtSettings; //Uygulama ayarlarından gelen JWT yapılandırmalarını tutar 

        public AuthService(IOptions<JwtSettings> jwtSettings) //appsettings.json veya appsettings.Development.json dosyasından ayarları okumak için kullanılır.
        {
            _jwtSettings = jwtSettings.Value; //içerisindeki gerçek değere erişmek için.
        }
        

        public string CreateToken(User user) //Kullanıcıyı parametre olarak alır ve bu kullanıcıya özel bir JWT üretir.
        
        {
            var claims = new[] //JWT’ye eklenecek kullanıcı bilgilerini belirtir.
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //genelde kullanıcı ID'si için kullanılır.
                new Claim(ClaimTypes.Name, user.Name ?? ""), 
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("Email", user.EMail ?? "") //özel bir claim, kullanıcının e-posta adresi.
                
            };
            
            var token = new JwtSecurityToken( //token’ı :
                issuer: _jwtSettings.Issuer, //kim üretti
                audience: _jwtSettings.Audience, // kim kullanacak
                claims: claims, //yukarıda tanımlanan kullanıcı bilgileri
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DeadLine), //token’in geçerlilik süresi. 
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