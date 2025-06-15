using InventarioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Data
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");
            });

            /*Category*/
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Linea Blanca",
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 2,
                Name = "Computadoras",
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 3,
                Name = "Electrodomesticos",
            });

            /*Product*/
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Code = "LB_183747",
                Description = "Refrigeradora Side by Side LG",
                Price = 41900.00m,
                CategoryId = 1
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Code = "LB_181327",
                Description = "Congelador Horizontal GRS 5CP GF 155ST",
                Price = 3000.00m,
                CategoryId = 1
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Code = "CP_192229",
                Description = "Laptop HP 14EM0017LA Ryzen 5-7520U",
                Price = 30450.00m,
                CategoryId = 2
            });
        }
    }
}
