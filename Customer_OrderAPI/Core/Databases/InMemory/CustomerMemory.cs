using b2.Core.Models;

namespace Customer_OrderAPI.Core.Databases.InMemory
{
    public class CustomerMemory
    {
        public Dictionary<string, Customer> CustomerMem { get; set; }

        public CustomerMemory()
        {
            CustomerMem = new Dictionary<string, Customer>();
        }
    }
}
