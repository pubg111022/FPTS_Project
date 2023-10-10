using b2.Core.Databases;
using b2.Core.IRepository;
using b2.Core.Models;
using Customer_OrderAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace b2.Core.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly AppDbContext _db;
        private readonly ILogger<OrderRepository> _logger;
        private readonly OrderMemory _inMem;
        private readonly CustomerMemory _customerMem;

        public OrderRepository(AppDbContext db, ILogger<OrderRepository> logger, OrderMemory inMem, CustomerMemory customerMem)
        {
            _db = db;
            _logger = logger;
            _inMem = inMem;
            _customerMem = customerMem;
        }
      

        public List<Order> GetAllInformation()
        {
            List<Order> res = new List<Order>();
            var cus = _customerMem.CustomerMem.Values.ToList();
            foreach (var item in _inMem.OrderMem.Values.ToList())
            {
                foreach (var item2 in cus) {
                    item2.Orders = null;
                    if (item.CustomerId.Equals(item2.Id))
                    {
                        item.Customer = item2;
                    }
                }
                res.Add(item);
            }
            return res;
        }


        public Order FindById(string id)
        {
            Order order = new Order();
            try
            {
                var rs = _inMem.OrderMem.Values.ToList();
                order = rs.Where(x=>x.Id.Equals(id)).First();
                return order;
            }
            catch
            {
                return order;
            }
        }

        public List<Order> GetAll()
        {
            List<Order> Order = new List<Order>();
            try
            {
                Order = _inMem.OrderMem.Values.ToList();
                return Order;
            }
            catch
            {
                return Order;
            }
        }

        public Order Insert(Order order)
        {
            try
            {
                if (order != null)
                {
                    _db.Order.Add(order);
                    _db.SaveChanges();
                    _inMem.OrderMem.Add(order.Id,order);
                }
                return order;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Order Update(Order Order)
        {
            try
            {
                if (Order != null)
                {
                    _db.Order.Update(Order);
                    _db.SaveChanges();
                }
                return Order;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Order Delete(Order order)
        {
            try
            {
                _db.Order.Remove(order);
                _db.SaveChanges();
                return order;
            }
            catch
            {
                return order;
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
