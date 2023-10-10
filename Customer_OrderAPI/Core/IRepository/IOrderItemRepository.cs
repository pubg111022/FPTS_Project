using b2.Core.Models;

namespace b2.Core.IRepository
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAll();
        OrderItem FindById(string id);
        OrderItem Insert(OrderItem orderItem);
        OrderItem Update(OrderItem orderItem);
        OrderItem Delete(OrderItem orderItem);
    }
}
