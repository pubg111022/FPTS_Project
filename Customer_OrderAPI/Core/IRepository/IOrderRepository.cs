using b2.Core.Models;

namespace b2.Core.IRepository
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order FindById(string id);
        List<Order> GetAllInformation();
        Order Insert(Order order);
        Order Update(Order order);
        Order Delete(Order order);
    }
}
