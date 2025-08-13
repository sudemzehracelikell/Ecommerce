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

    public User? Authenticate(string name, string password)
    {
        var user = _queryRepository.FilterBy(u => u.Name ==name).FirstOrDefault();
        if (user == null) return null;

        // BCrypt ile şifre doğrulama
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }

    public void CreateUser(User user, string password)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        _enumRepository.Update(user);
    
    }
}