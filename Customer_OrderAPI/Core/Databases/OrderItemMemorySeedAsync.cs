using b2.Core.Databases;
using Customer_OrderAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace Customer_OrderAPI.Core.Databases
{
    public class OrderItemMemorySeedAsync
    {
        public async Task SeedAsync(OrderItemMemory memory, AppDbContext dbContext)
        {
            var data = await dbContext.OrderItem.ToListAsync();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    memory.OrderItemMem.Add(item.Id, item);
                }
            }
        }
    }
}
