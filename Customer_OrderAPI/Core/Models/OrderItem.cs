namespace b2.Core.Models
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string ?ProductName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string OrderId { get; set; }
        public Order ?Order { get; set; }
    }
}
