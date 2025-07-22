using HugeProject.Models;

namespace HugeProject.Services.Interfaces;

public interface IUserService : IBaseService<User>
{
    IQueryable<User>? GetUserByName(string name);
}