using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.models;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/userController")]
public class UserController : ControllerBase
{
    private readonly IGenericRepository<User> _repository;

    public UserController(IGenericRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await _repository.GetAll();
        return users;
    }

    [HttpGet("{id}")]
    public async Task<User> GetByIdUser(int id)
    {
        var user = await _repository.GetById(id);
        if (user != null)
            return user;
        return null;
    }

    [HttpPost]
    public async Task CreateUser(User newUser)
    {
        await _repository.Create(newUser);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(int id)
    {
        await _repository.Delete(id);
    }

    [HttpPut]
    public async Task UpdateUser(User newUser)
    {
        await _repository.Update(newUser);
    }

}