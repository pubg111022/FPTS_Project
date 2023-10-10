using b2.Core.Models;
using b2.DTOS;

namespace b2.Core.IServices
{
    public interface IOrderItemService
    {
        List<OrderItemDTO> GetAll();
        OrderItemDTO FindById(string id);
        OrderItemDTO Insert(OrderItemDTO orderItem);
        OrderItemDTO Update(OrderItemDTO orderItem);
        OrderItemDTO Delete(OrderItemDTO orderItem);
    }
}
