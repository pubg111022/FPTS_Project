using b2.DTOS;

namespace Customer_OrderAPI.DTOS
{
    public class InsertOrderDTO
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? AdditionalAddress { get; set; }

    }
}
