using b2.Core.Models;
using b2.DTOS;

namespace b2.Core.IRepository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer FindById(string id);
        Customer Insert(Customer customer);
        Customer Update(string id,Customer customer);
        Customer Delete(string id);
    }
}
