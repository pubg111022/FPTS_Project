using Microsoft.EntityFrameworkCore;
using ProductAPI.Core.Database.EntitiesTypeConfigurations;
using ProductAPI.Core.Models;

namespace ProductAPI.Core.Database
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Products>(new ProductEntityTypeConfiguration());
        }
    }
}
