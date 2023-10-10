namespace b2.Core.Models
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ?CustomerId { get; set; }
        public string ?Street { get; set; }
        public string ?City { get; set; }
        public string ?District { get; set; }
        public string ?AdditionalAddress { get; set; }

        public ICollection<OrderItem> ?OrderItems { get; set; }
        public Customer ?Customer { get; set; }
    }
}
