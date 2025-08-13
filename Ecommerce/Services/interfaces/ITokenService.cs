using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}