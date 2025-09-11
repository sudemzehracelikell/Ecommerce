using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces;

public interface IOrderService : IBaseService<Order>
{
    IQueryable<Order>? GetUserOrders(string userId);

    IQueryable<Order>? GetOrdersBefore(DateTime date);
    IQueryable<Order>? GetOrdersAfter(DateTime date);
    IQueryable<Order>? GetOrderOn(DateTime date);
}