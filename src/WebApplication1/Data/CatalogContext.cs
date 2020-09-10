using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Settings;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
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
