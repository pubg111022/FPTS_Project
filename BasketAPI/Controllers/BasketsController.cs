using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using b3.Core.Databases;
using b3.Core.Models;
using b3.Services;
using b3.Core.IServices;
using b3.DTOs;
using BasketAPI.DTOs;

namespace b3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly BasketDbContext _context;
        private readonly IBasketService _basketService;
        public BasketsController(BasketDbContext context, IBasketService basketService)
        {
            _context = context;
            _basketService = basketService;
        }

        // GET: api/Baskets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Basket>>> GetBasket()
        {
          if (_context.Basket == null)
          {
              return NotFound();
          }
            return await _context.Basket.ToListAsync();
        }

        // GET: api/Baskets/5
        [HttpGet("{CustomerId}")]
        public BasketDTO GetBasket(string CustomerId)
        {
          
            return _basketService.Find(CustomerId);
        }
        // POST: api/Baskets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public InsertBasketDTO PostBasket(InsertBasketDTO basket)
        {
            return _basketService.Insert(basket);
        }

        // DELETE: api/Baskets/5
      
        private bool BasketExists(string id)
        {
            return (_context.Basket?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
