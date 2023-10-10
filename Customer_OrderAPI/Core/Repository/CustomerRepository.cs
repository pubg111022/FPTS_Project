using b2.Core.Databases;
using b2.Core.IRepository;
using b2.Core.Models;
using Customer_OrderAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace b2.Core.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDbContext _db;
        private readonly ILogger<CustomerRepository> _logger;
        private readonly CustomerMemory _inMem;

        public CustomerRepository(AppDbContext db, ILogger<CustomerRepository> logger, CustomerMemory inMem) { 
        
            _db = db;
            _logger = logger;
            _inMem = inMem;
        }
        public Customer Delete(string id)
        {
            var item = _db.Customer.Find(id);
            try
            {
                _db.Customer.Remove(item);
                _db.SaveChanges();
                return item;
            }
            catch
            {
                return item;
            }
        }

        public Customer FindById(string id)
        {
            Customer Customer = new Customer();
            try
            {
                var rs = _inMem.CustomerMem.Values.ToList();

                Customer = rs.Where(x => x.Id.Equals(id)).First();
                return Customer;
            }
            catch
            {
                return Customer;
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                var rs = _inMem.CustomerMem.Values.ToList();
                return rs;
            }
            catch
            {
                throw;

            }
        }

        public Customer Insert(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    _db.Customer.Add(customer);
                    _db.SaveChanges();
                    _inMem.CustomerMem.Add(customer.Id,customer);
                }
                return customer;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public Customer Update(string id ,Customer customer)
        {
            var item = _db.Customer.Find(id);
            if(customer.Name == null)
            {
                customer.Name = item.Name;
            }
            if(customer.PhoneNumber == null)
            {
                customer.PhoneNumber = item.PhoneNumber;
            }
            try
            {
                if (customer != null)
                {
                    _db.Customer.Update(customer);
                    _db.SaveChanges();
                }
                return customer;
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
