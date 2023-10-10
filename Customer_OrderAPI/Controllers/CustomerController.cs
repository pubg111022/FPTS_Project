using b2.Core.IServices;
using b2.Core.Models;
using b2.DTOS;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace b2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService service, ILogger<CustomerController> logger) 
        {
            _service = service;
            _logger = logger;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public List<CustomersDTO> Get()
        {
            return _service.GetAll();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public CustomersDTO Get(string id)
        {
            return _service.FindById(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public CustomersDTO Post([FromBody] Customer customer)
        {
            return _service.Insert(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public CustomerUpdateNameDTO Put(string id,[FromBody] CustomerUpdateNameDTO customer)
        {
            return _service.Update(id,customer);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public CustomerDeleteDTO Delete(string id)
        {
            return _service.Delete(id);
        }
    }
}
