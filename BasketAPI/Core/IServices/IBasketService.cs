using b3.Core.Models;
using b3.DTOs;
using BasketAPI.DTOs;

namespace b3.Core.IServices
{
    public interface IBasketService
    {
        public InsertBasketDTO Insert(InsertBasketDTO basket);
        public BasketDTO Find(string CustomerId);
    }
}
