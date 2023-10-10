using Microsoft.EntityFrameworkCore;
using ProductAPI.Core.Database.InMemory;

namespace ProductAPI.Core.Database
{
    public class ProductMemorySeedAsync
    {
        public async Task SeedAsync(ProductMemory memory, ProductDbContext dbContext)
        {
            var products = await dbContext.Products.ToListAsync();
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    memory.Memory.Add(product.Id, product);
                }
            }
        }
    }
}
