using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions settings):base(settings)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Order> Orders { get; set; }
    }
}
