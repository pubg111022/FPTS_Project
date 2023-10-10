using b3.Core.Models;

namespace b3.DTOs
{
    public class BasketDTO
    {
        public string CustomerId { get; set; }

        public ICollection<BasketItemDTO> ?BasketItems { get; set; }
    }
}
