using b3.Core.Databases;
using BasketAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Core.Databases
{
    public class BasketMemorySeedAsync
    {
        public async Task SeedAsync(BasketMemory memory, BasketDbContext dbContext)
        {
            var baskets = await dbContext.Basket.ToListAsync();
            if (baskets.Count > 0)
            {
                foreach (var item in baskets)
                {
                    memory.BasketMem.Add(item.CustomerId, item);
                }
            }
        }
    }
}
