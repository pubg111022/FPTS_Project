using b3.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace b3.Core.Databases.EntitiesTypeConfigurations
{
    public class BasketItemTypeConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable("BasketItems");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.ProductId).HasColumnName("ProductId");
            builder.Property(p => p.ProductName).HasColumnName("ProductName");
            builder.Property(p => p.Quantity).HasColumnName("Quantity");
            builder.Property(p => p.Status).HasColumnName("Status");
            builder.Property(p => p.CustomerBasketId).HasColumnName("CustomerBasketId");

            builder.HasOne<Basket>(bi => bi.Basket) 
           .WithMany(b => b.Items)         
           .HasForeignKey(b => b.CustomerBasketId); 
        }
    }
}
