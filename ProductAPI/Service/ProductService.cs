using ProductAPI.Core.Database;
using ProductAPI.Core.IRepositories;
using ProductAPI.Core.IServices;
using ProductAPI.Core.Models;

namespace ProductAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductDbContext productDbContext, IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productDbContext = productDbContext;
            _productRepository = productRepository;
            _logger = logger;
        }

        public List<Products> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public Products Insert(Products p)
        {
            return _productRepository.Insert(p);
        }

        public Products UpdateAvailableQuantity(string ProductId, float AvailableQuantity)
        {
            return _productRepository.UpdateAvailableQuantity(ProductId, AvailableQuantity);
        }

        public Products UpdateName(string ProductId, string Name)
        {
            return _productRepository.UpdateName(ProductId, Name);
        }

        public Products UpdatePrice(string ProductId, float Price)
        {
            return _productRepository.UpdatePrice(ProductId, Price);
        }
    }
}
