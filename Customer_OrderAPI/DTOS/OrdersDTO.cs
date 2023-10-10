﻿using b2.Core.Models;

namespace b2.DTOS
{
    public class OrdersDTO
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? AdditionalAddress { get; set; }

        public ICollection<OrderItemDTO>? OrderItems { get; set; }
    }
}
