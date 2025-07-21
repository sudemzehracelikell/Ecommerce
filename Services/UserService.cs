using Ecommerce.Models;
using Ecommerce.Repository;

namespace Ecommerce.Services;

public class UserService : BaseService<User>, Interfaces.IUserService
{
    public UserService(IEnumarableRepository<User> _enumRepository, IQueryableRepository<User> _queryableRepository)
    : base(_enumRepository, _queryableRepository)
    { }

    public IQueryable<User>? GetUserByName(string name)
    {
        var u = _queryRepository.FilterBy(u => u.Name == name);
        return u ?? null;
    }
    
}