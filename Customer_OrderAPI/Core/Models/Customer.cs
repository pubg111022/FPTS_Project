namespace b2.Core.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string ?PhoneNumber { get; set; }

        public string ?Name { get; set; }

        public ICollection<Order> ?Orders { get; set; }
    }
}
