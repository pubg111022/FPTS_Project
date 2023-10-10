using b3.Core.Models;
using b3.DTOs;
using BasketAPI.DTOs;

namespace b3.Core.IServices
{
    public interface IBasketItemService
    {
        public BasketItemInsertDataDTO Insert(BasketItemInsertDataDTO item);
        public BasketItemDTO Update(BasketItemDTO b, string CustomerId, string ProductId);
        public List<BasketItemDTO> FindByCustomer(string CustomerId);
        public List<BasketItemDTO> DeleteByBasketCustomerId(string Id);
    }
}
