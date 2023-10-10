using b3.DTOs;

namespace BasketAPI.DTOs
{
    public class BasketItemInsertDataDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string CustomerBasketId { get; set; }
    }
}
