using Microsoft.EntityFrameworkCore;

namespace TP_Lab2.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public ShopContext()
        {
            //
        }
    }
}