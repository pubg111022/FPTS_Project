using b3.Core.Models;

namespace b3.Core.IRepositories
{
    public interface IBasketItemRepository
    {
        public BasketItem Insert(BasketItem item);
        public List<BasketItem> FindByCustomer(string CustomerId);
        public BasketItem Update(BasketItem b, string CustomerId, string ProductId);
        public List<BasketItem> DeleteByBasketCustomerId(string Id);
    }
}
