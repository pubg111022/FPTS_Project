using b3.Core.Databases;
using b3.Core.Models;
using BasketAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Core.Databases
{
    public class BasketItemMemorySeedAsync
    {
        public async Task SeedAsync(BasketItemMemory memory, BasketDbContext dbContext)
        {
            var basketItems = await dbContext.BasketItem.ToListAsync();
            if (basketItems.Count > 0)
            {
                foreach (var item in basketItems)
                {
                    memory.BasketItemMem.Add(item.Id, item);
                }
            }
        }
    }
}
