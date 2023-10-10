using b2.Core.Models;
using System.Collections.ObjectModel;

namespace b2.DTOS
{
    public class OrderItemDTO
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
        public ProductsDTO Products { get; set; }
        
    }
}
