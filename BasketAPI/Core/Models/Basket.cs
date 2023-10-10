namespace b3.Core.Models
{
    public class Basket
    {
        public string CustomerId { get; set; }

        public List<BasketItem> Items { get; set; }

        public Basket ()
        {
            Items = new List<BasketItem> ();
        }
    }
}
