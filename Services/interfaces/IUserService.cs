using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces;

public interface IUserService : IBaseService<User>
{
    IQueryable<User>? GetUserByName(string name);
}