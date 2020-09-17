using Microsoft.EntityFrameworkCore;
using WebApplication1.API.Entities;

namespace WebApplication1.API.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions settings):base(settings)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
    }
}
