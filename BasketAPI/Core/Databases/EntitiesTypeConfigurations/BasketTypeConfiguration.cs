using b3.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace b3.Core.Databases.EntitiesTypeConfigurations
{
    public class BasketTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Baskets");
            builder.HasKey(p => p.CustomerId);
            builder.Property(p => p.CustomerId).HasColumnName("CustomerId");


            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
