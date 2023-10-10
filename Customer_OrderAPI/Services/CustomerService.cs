using b2.Core.IRepository;
using b2.Core.IServices;
using b2.Core.Models;
using b2.Core.Regex;
using b2.DTOS;

namespace b2.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;
        private readonly Validation _validator;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public CustomerDeleteDTO Delete(string id)
        {
            var item = _repository.Delete(id);
            CustomerDeleteDTO dto = new CustomerDeleteDTO();
            dto.Id = item.Id;
            return dto;
        }

        public CustomersDTO FindById(string id)
        {
            var rs = _repository.FindById(id);
            CustomersDTO customersDTO = new CustomersDTO();
            customersDTO.Id = rs.Id;
            customersDTO.Name = rs.Name;
            customersDTO.PhoneNumber = rs.PhoneNumber;
            return customersDTO;
        }

        public List<CustomersDTO> GetAll()
        {
            var rs = _repository.GetAll();
            List<CustomersDTO> list = new List<CustomersDTO>();
            foreach (var r in rs)
            {
                CustomersDTO customersDTO = new CustomersDTO();
                customersDTO.Id = r.Id;
                customersDTO.Name = r.Name;
                customersDTO.PhoneNumber = r.PhoneNumber;
                list.Add(customersDTO);
            }
            return list;
        }

        public CustomersDTO Insert(Customer customer)
        {
            CustomersDTO customersDTO = new CustomersDTO();
            if (_validator.IsPhoneValid(customer.PhoneNumber)){
                var rs = _repository.Insert(customer);
                customersDTO.Id = rs.Id;
                customersDTO.Name = rs.Name;
                customersDTO.PhoneNumber = rs.PhoneNumber;
                return customersDTO;
            }
            return customersDTO;
        }

        public CustomerUpdateNameDTO Update(string id ,CustomerUpdateNameDTO customer)
        {
            Customer c = new Customer();
            c = _repository.FindById(id);
            if(c != null)
            {
                c.Name = customer.Name;
            }
           
            _repository.Update(id ,c);
            return customer;
        }
    }
}
