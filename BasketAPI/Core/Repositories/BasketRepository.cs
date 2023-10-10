using b3.Core.Databases;
using b3.Core.IRepositories;
using b3.Core.Models;
using BasketAPI.Core.Databases.InMemory;
using System;

namespace b3.Core.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly BasketDbContext _db;
        private readonly ILogger<BasketRepository> _logger;
        private readonly BasketMemory _inMem;

        public BasketRepository(BasketDbContext db, ILogger<BasketRepository> logger, BasketMemory inMem)
        {
            _db = db;
            _logger = logger;
            _inMem = inMem;
        }

        public Basket Find(string CustomerId)
        {
            try
            {
                var rs = _inMem.BasketMem.Values.ToList();

                foreach ( var item in rs)
                {
                    if (item.CustomerId == CustomerId)
                        return item;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Basket Insert(Basket basket)
        {
            _db.Basket.Add(basket);
            _db.SaveChanges();
            _inMem.BasketMem.Add(basket.CustomerId, basket);

            return basket; 
        }
    }
}
