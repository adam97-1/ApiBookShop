using Microsoft.EntityFrameworkCore;

namespace WebApiBookShop.Models
{
    public class BookShopContext : DbContext
    {
        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options) { 
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set;}
        public DbSet<Cart> Carts { get; set;}
        public DbSet<Client> Clients { get; set;}
        public DbSet<CartBook> CartBooks { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<OrderBook> OrderBooks { get; set;}
    }
}
