using b3.Core.Databases.EntitiesTypeConfigurations;
using b3.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace b3.Core.Databases
{
    public class BasketDbContext:DbContext
    {

        public BasketDbContext()
        {
        }

        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {

        }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Basket>(new BasketTypeConfiguration());
            modelBuilder.ApplyConfiguration<BasketItem>(new BasketItemTypeConfiguration());
        }
    }
}
