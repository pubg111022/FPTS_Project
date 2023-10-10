using b3.Core.Databases;
using b3.Core.IRepositories;
using b3.Core.Models;
using BasketAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace b3.Core.Repositories
{
    public class BasketItemRepository : IBasketItemRepository
    {

        private readonly BasketDbContext _db;
        private readonly ILogger<BasketItemRepository> _logger;
        private readonly BasketItemMemory _inMem;
        public BasketItemRepository(BasketDbContext db, ILogger<BasketItemRepository> logger, BasketItemMemory inMem)
        {
            _db = db;
            _logger = logger;
            _inMem = inMem;
        }

        public List<BasketItem> FindByCustomer(string id)
        {
            List<BasketItem> basketItems = new List<BasketItem>();
            try
            {
                var rs = _inMem.BasketItemMem.Values.ToList();

                foreach (var item in rs)
                {
                    if(item.CustomerBasketId == id)
                    {
                        basketItems.Add(item);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return basketItems;
        }

        public BasketItem Insert(BasketItem item)
        {
            //item.Basket = null;
            try
            {
                _db.BasketItem.Add(item);
                _db.SaveChanges();
                _inMem.BasketItemMem.Add(item.Id, item);
            }
            catch (Exception)
            {

                throw;
            }
            
            return item;
        }

        public BasketItem Update(BasketItem b, string CustomerId, string ProductId)
        {
            var item = _db.BasketItem.Where(x=>x.CustomerBasketId.Equals(CustomerId)).Where(x=>x.ProductId.Equals(ProductId)).FirstOrDefault();
            
            if (item != null)
            {
                try
                {
                    item.Quantity = b.Quantity;
                    _db.BasketItem.Update(item);
                    _db.SaveChanges();
                    return item;
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            return b;
        }
        public List<BasketItem> DeleteByBasketCustomerId(string Id)
        {
            var rs = _db.BasketItem.Where(x => x.CustomerBasketId.Equals(Id)).ToList();
            try
            {
                _db.BasketItem.Where(x => x.CustomerBasketId.Equals(Id)).ExecuteDelete();
                return rs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
