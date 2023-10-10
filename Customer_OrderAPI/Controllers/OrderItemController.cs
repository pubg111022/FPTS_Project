using b2.Core.IServices;
using b2.Core.Models;
using b2.DTOS;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace b2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _service;
        private readonly ILogger<OrderItemController> _logger;
        public OrderItemController(IOrderItemService service, ILogger<OrderItemController> logger)
        {
            _service = service;
            _logger = logger;
        }
        // GET: api/<OrderItemController>
        [HttpGet]
        public List<OrderItemDTO> Get()
        {
             return _service.GetAll();
        }

        // GET api/<OrderItemController>/5
        [HttpGet("{id}")]
        public OrderItemDTO Get(string id)
        {
            return _service.FindById(id);
        }

        // POST api/<OrderItemController>
        [HttpPost]
        public OrderItemDTO Post([FromBody] OrderItemDTO value)
        {
            return _service.Insert(value);
        }

        // PUT api/<OrderItemController>/5
        [HttpPut("{id}")]
        public OrderItemDTO Put([FromBody] OrderItemDTO value)
        {
            return _service.Update(value);
        }

        // DELETE api/<OrderItemController>/5
        [HttpDelete("{id}")]
        public OrderItemDTO Delete(OrderItemDTO value)
        {
            return _service.Delete(value);
        }
    }
}
