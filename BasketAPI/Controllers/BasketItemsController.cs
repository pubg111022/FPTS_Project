using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using b3.Core.Databases;
using b3.Core.Models;
using b3.Core.IServices;
using b3.DTOs;
using BasketAPI.DTOs;

namespace b3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemsController : ControllerBase
    {
        private readonly BasketDbContext _context;
        private readonly IBasketItemService _basketItemService;
        public BasketItemsController(BasketDbContext context, IBasketItemService basketItemService)
        {
            _context = context;
            _basketItemService = basketItemService;
        }


        // GET: api/BasketItems/5
        [HttpGet("{CustomerId}")]
        public List<BasketItemDTO> GetBasketItemByCutomer(string CustomerId)
        {

            return _basketItemService.FindByCustomer(CustomerId);
        }

        // PUT: api/BasketItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        public BasketItemDTO PutBasketItem(BasketItemDTO basketItem)
        {
            return _basketItemService.Update(basketItem, basketItem.CustomerBasketId, basketItem.ProductId);
        }

        // POST: api/BasketItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public BasketItemInsertDataDTO PostBasketItem(BasketItemInsertDataDTO basketItem)
        {
            _basketItemService.Insert(basketItem);
            return basketItem;
        }

        [HttpDelete("{id}")]
        public List<BasketItemDTO> DeleteBasketItemByBasketCustomerId(string id)
        {
            return _basketItemService.DeleteByBasketCustomerId(id);
        }
        private bool BasketItemExists(int id)
        {
            return (_context.BasketItem?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
