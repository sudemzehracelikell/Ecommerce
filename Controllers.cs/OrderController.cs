using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.models;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/orderController")]
public class OrderController : ControllerBase
{
    private readonly IGenericRepository<Order> _repository;

    public OrderController(IGenericRepository<Order> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        var orders = await _repository.GetAll();
        return orders;
    }

    [HttpGet("{id}")]
    public async Task<Order> GetByIdOrder(int id)
    {
        var order = await _repository.GetById(id);
        if (order != null)
            return order;
        return null;
    }

    [HttpPost]
    public async Task CreateOrder(Order newOrder)
    {
        await _repository.Create(newOrder);
    }

    [HttpDelete("{id}")]
    public async Task DeleteOrder(int id)
    {
        await _repository.Delete(id);
    }

    [HttpPut]
    public async Task UpdateOrder(Order newOrder)
    {
        await _repository.Update(newOrder);
    }

}