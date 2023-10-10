using b2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace b2.Core.Databases.EntitiesTypeConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.OrderDate).HasColumnName("OrderDate");
            builder.Property(p => p.CustomerId).HasColumnName("CustomerId");
            builder.Property(p => p.Street).HasColumnName("Street");
            builder.Property(p => p.City).HasColumnName("City");
            builder.Property(p => p.District).HasColumnName("District");
            builder.Property(p => p.AdditionalAddress).HasColumnName("AdditionalAddress");
        }
    }
}
