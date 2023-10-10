using b2.Core.Databases;
using Customer_OrderAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace Customer_OrderAPI.Core.Databases
{
    public class OrderMemorySeedAsync
    {
        public async Task SeedAsync(OrderMemory memory, AppDbContext dbContext)
        {
            var data = await dbContext.Order.ToListAsync();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    memory.OrderMem.Add(item.Id, item);
                }
            }
        }
    }
}
