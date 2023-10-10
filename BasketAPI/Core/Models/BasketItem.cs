namespace b3.Core.Models
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string CustomerBasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
