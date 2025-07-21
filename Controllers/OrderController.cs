using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : GenericController<Order>
{
    private new readonly IOrderService _service;

    public OrderController(IOrderService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("user-orders/{userId}")]
    public IActionResult GetUserOrders(int userId)
    {
        var orders = _service.GetUserOrders(userId);
        return orders != null ? Ok(orders.ToList()) : NotFound();
    }

    [HttpGet("before/{date}")]
    public IActionResult GetOrdersBefore(DateTime date)
    {
        var orders = _service.GetOrdersBefore(date);
        return orders != null ? Ok(orders.ToList()) : NotFound();
    }

    [HttpGet("after/{date}")]
    public IActionResult GetOrdersAfter(DateTime date)
    {
        var orders = _service.GetOrdersAfter(date);
        return orders != null ? Ok(orders.ToList()) : NotFound();
    }

    [HttpGet("on/{date}")]
    public IActionResult GetOrderOn(DateTime date)
    {
        var orders = _service.GetOrderOn(date);
        return orders != null ? Ok(orders.ToList()) : NotFound();
    }
}