using b3.Core.Models;

namespace BasketAPI.Core.Databases.InMemory
{
    public class BasketItemMemory
    {
        public Dictionary<string, BasketItem> BasketItemMem { get; set; }

        public BasketItemMemory()
        {
            BasketItemMem = new Dictionary<string, BasketItem>();
        }
    }
}
