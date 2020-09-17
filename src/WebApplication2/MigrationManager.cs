using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication2.API.Data;
using WebApplication2.API.Data.Interfaces;

namespace WebApplication2.API
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
             var scope = host.Services.CreateScope();
             var appContext = scope.ServiceProvider.GetRequiredService<OrderingContext>();
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
