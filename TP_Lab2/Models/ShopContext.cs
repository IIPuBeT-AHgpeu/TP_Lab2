using Microsoft.EntityFrameworkCore;

namespace TP_Lab2.Models
{
    public class ShopContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=shop_db;Username=postgres;Password=root");
        }
    }
}