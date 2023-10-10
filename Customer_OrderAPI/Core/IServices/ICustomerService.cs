using b2.Core.Models;
using b2.DTOS;

namespace b2.Core.IServices
{
    public interface ICustomerService
    {
        List<CustomersDTO> GetAll();
        CustomersDTO FindById(string id);
        CustomersDTO Insert(Customer customer);
        CustomerUpdateNameDTO Update(string id,CustomerUpdateNameDTO customer);
        CustomerDeleteDTO Delete(string id);
    }
}
