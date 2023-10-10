using b2.Core.Models;

namespace b2.DTOS
{
    public class CustomersDTO
    {
        public string Id { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Name { get; set; }

        public ICollection<OrdersDTO>? Orders { get; set; }
    }
}
