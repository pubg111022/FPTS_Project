using b2.Core.Databases;
using Customer_OrderAPI.Core.Databases.InMemory;
using Microsoft.EntityFrameworkCore;

namespace Customer_OrderAPI.Core.Databases
{
    public class CustomerMemorySeedAsync
    {
        public async Task SeedAsync(CustomerMemory memory, AppDbContext dbContext)
        {
            var data = await dbContext.Customer.ToListAsync();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    memory.CustomerMem.Add(item.Id, item);
                }
            }
        }
    }
}
