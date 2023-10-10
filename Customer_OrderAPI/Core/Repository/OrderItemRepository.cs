using b2.Core.Databases;
using b2.Core.IRepository;
using b2.Core.Models;
using Customer_OrderAPI.Core.Databases.InMemory;

namespace b2.Core.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<OrderItemRepository> _logger;
        private readonly OrderItemMemory _inMem;
        public OrderItemRepository(AppDbContext db, ILogger<OrderItemRepository> logger, OrderItemMemory inMem)
        {

            _db = db;
            _logger = logger;
            _inMem = inMem;
        }
        public OrderItem Delete(OrderItem OrderItem)
        {
            try
            {
                _db.OrderItem.Remove(OrderItem);
                _db.SaveChanges();
                return OrderItem;
            }
            catch (Exception ex) 
            {
                throw;

            }
        }

        public OrderItem FindById(string id)
        {
            OrderItem OrderItem = new OrderItem();
            try
            {
                var rs = _inMem.OrderItemMem.Values.ToList();

                OrderItem = rs.Where(x=>x.Id.Equals(id)).First();
                return OrderItem;
            }
            catch(Exception ex) 
            {
                throw;

            }
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem> OrderItem = new List<OrderItem>();
            try
            {
                OrderItem = _inMem.OrderItemMem.Values.ToList();
                return OrderItem;
            }
            catch(Exception ex) 
            {
                throw;

            }
        }

        public OrderItem Insert(OrderItem OrderItem)
        {
            try
            {
                if (OrderItem != null)
                {
                    _db.OrderItem.Add(OrderItem);
                    _db.SaveChanges();
                    _inMem.OrderItemMem.Add(OrderItem.Id, OrderItem);

                }
                return OrderItem;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;

            }
        }

        public OrderItem Update(OrderItem OrderItem)
        {
            try
            {
                if (OrderItem != null)
                {
                    _db.OrderItem.Update(OrderItem);
                    _db.SaveChanges();
                }
                return OrderItem;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;

            }
        }
        public void SaveChanges()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
