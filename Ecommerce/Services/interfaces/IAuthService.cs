using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IAuthService
    {
        string CreateToken(User user);
    }
}