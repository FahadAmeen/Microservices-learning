using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Catalog.API.Data;
using WebApplication1.API.Data.Interfaces;

namespace AL.ERA.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
            try
            {

                appContext.Database.Migrate();
                   
                var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedsData().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }

            return host;
        }

    }
}
