using Microsoft.EntityFrameworkCore;
using WebApplication2.API.Entities;

namespace WebApplication2.API.Data
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> settings):base(settings)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Order> Orders { get; set; }
    }
}
