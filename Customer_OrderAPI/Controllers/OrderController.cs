using b2.Core.IServices;
using b2.Core.Models;
using b2.DTOS;
using Customer_OrderAPI.DTOS;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace b2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService service, ILogger<OrderController> logger)
        {
            _service = service;
            _logger = logger;
        }


        // GET: api/<OrderController>
        [HttpGet]
        public List<OrdersDTO> Get()
        {
            return _service.GetAll();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public OrdersDTO  Get(string id)
        {
            return _service.FindById(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public InsertOrderDTO Post([FromBody] InsertOrderDTO order)
        {
            return _service.Insert(order);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public OrdersDTO Put([FromBody] OrdersDTO order)
        {
            return _service.Update(order);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public OrdersDTO Delete(OrdersDTO order)
        {
            return _service.Delete(order);

        }
    }
}
