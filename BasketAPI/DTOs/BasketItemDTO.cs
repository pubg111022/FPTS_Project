using b3.Core.Models;

namespace b3.DTOs
{
    public class BasketItemDTO
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string CustomerBasketId { get; set; }
        public BasketDTO ?Basket { get; set; }
    }
}
