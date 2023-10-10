using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Core.Models;

namespace ProductAPI.Core.Database.EntitiesTypeConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.Price).HasColumnName("Price");
            builder.Property(p => p.AvailableQuantity).HasColumnName("AvailableQuantity");

        }
    }
}
