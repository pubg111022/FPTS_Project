using b3.DTOs;

namespace BasketAPI.DTOs
{
    public class InsertBasketDTO
    {
        public string CustomerId { get; set; }

        public ICollection<BasketItemInsertDataDTO>? BasketItems { get; set; }
    }
}
