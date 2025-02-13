using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) 
            : base(options)
        {
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Delivery> deliveries { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ProductOrder> product_orders { get; set; }
        public DbSet<Review> reviews { get; set; }
    }
}
