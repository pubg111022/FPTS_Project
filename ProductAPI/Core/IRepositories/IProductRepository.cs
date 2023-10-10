using ProductAPI.Core.Models;

namespace ProductAPI.Core.IRepositories
{
    public interface IProductRepository
    {
        Products Insert(Products p);
        List<Products> GetProducts();

        Products UpdatePrice(string ProductId , float Price);
        
        Products UpdateName(string ProductId , string Name);

        Products UpdateAvailableQuantity(string ProductId , float AvailableQuantity);

    }
}
