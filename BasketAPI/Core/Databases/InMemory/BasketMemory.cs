using b3.Core.Models;

namespace BasketAPI.Core.Databases.InMemory
{
    public class BasketMemory
    {
        public Dictionary<string, Basket> BasketMem { get; set; }

        public BasketMemory()
        {
            BasketMem = new Dictionary<string, Basket>();
        }
    }
}
