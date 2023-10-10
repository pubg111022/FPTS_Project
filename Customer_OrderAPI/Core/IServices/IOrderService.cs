using b2.Core.Models;
using b2.DTOS;
using Customer_OrderAPI.DTOS;

namespace b2.Core.IServices
{
    public interface IOrderService
    {
        List<OrdersDTO> GetAll();
        OrdersDTO FindById(string id);
        List<Order> GetAllInformation();
        InsertOrderDTO Insert(InsertOrderDTO order);
        OrdersDTO Update(OrdersDTO order);
        OrdersDTO Delete(OrdersDTO order);
    }
}
