using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Core.Database;
using ProductAPI.Core.IRepositories;
using ProductAPI.Core.IServices;
using ProductAPI.Core.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _iproductService;
        public ProductsController(IProductService iproductService)
        {
            _iproductService = iproductService;
        }

        [HttpPost]
        public Products InsertProduct(Products p)
        {
            return _iproductService.Insert(p);
        }



        // GET: api/Products
        [HttpGet]
        public List<Products> GetProducts()
        {
            return _iproductService.GetProducts();
        }

        
        [HttpPut]
        [Route("updateName")]
        public Products UpdateProductName(string id, string productName)
        {
            return _iproductService.UpdateName(id, productName);
        }

        [HttpPut]
        [Route("updatePrice")]
        public Products UpdateProductPrice(string id, float price)
        {
            return _iproductService.UpdatePrice(id, price);
        }

        [HttpPut]
        [Route("updateAvailableQuantity")]
        public Products UpdateProductAvailableQuantity(string id, float availableQuantity)
        {
            return _iproductService.UpdateAvailableQuantity(id, availableQuantity);
        }
    }
}
