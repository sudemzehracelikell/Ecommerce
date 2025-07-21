using HugeProject.Models;
using HugeProject.Repository;

namespace HugeProject.Services;

public class OrderService : BaseService<Order>, Interfaces.IOrderService
{
    public OrderService(IEnumarableRepository<Order> _enumRepository, IQueryableRepository<Order> _queryRepository)
    : base(_enumRepository, _queryRepository)
    { }

    public IQueryable<Order>? GetUserOrders(int userId)
    {
        var o = _queryRepository.FilterBy(o => o.UserId == userId);
        return o ?? null; // o != null ? o : null
    }

    public IQueryable<Order>? GetOrdersBefore(DateTime date)
    {
        var o = _queryRepository.FilterBy(o => o.OrderPlaced < date);
        return o ?? null;
    }

    public IQueryable<Order>? GetOrdersAfter(DateTime date)
    {
        var o = _queryRepository.FilterBy(o => o.OrderPlaced > date);
        return o ?? null;
    }

    public IQueryable<Order>? GetOrderOn(DateTime date)
    {
        var o = _queryRepository.FilterBy(o => o.OrderPlaced == date);
        return o ?? null;
    }
}