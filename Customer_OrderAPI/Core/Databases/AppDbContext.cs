using b2.Core.Databases.EntitiesTypeConfigurations;
using b2.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace b2.Core.Databases
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Customer>(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<Order>(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<OrderItem>(new OrderItemEntityTypeConfiguration());
            //modelBuilder.HasDefaultSchema("C##TEST");

            modelBuilder.Entity<Order>()
           .HasOne<Customer>(s => s.Customer)
           .WithMany(g => g.Orders)
           .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<OrderItem>()
           .HasOne<Order>(s => s.Order)
           .WithMany(g => g.OrderItems)
           .HasForeignKey(s => s.OrderId);
        }
    }
}
