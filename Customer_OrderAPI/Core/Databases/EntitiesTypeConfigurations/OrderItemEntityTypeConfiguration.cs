using b2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace b2.Core.Databases.EntitiesTypeConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.ProductName).HasColumnName("ProductName");
            builder.Property(p => p.ProductId).HasColumnName("ProductId");
            builder.Property(p => p.Quantity).HasColumnName("Quantity");
        }
    }
}
