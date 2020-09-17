using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using WebApplication2.API.Data;
using WebApplication2.API.Data.Interfaces;
using WebApplication2.API.Extentions;
using WebApplication2.API.RabbitMQ;
using WebApplication2.API.Repositories;
using WebApplication2.API.Repositories.Interfaces;

namespace WebApplication2.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetValue<string>("ConnectionStrings:ServerConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<OrderingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ServerConnection")), ServiceLifetime.Singleton);
            services.AddScoped<IDataSeeder, DataSeederService>();
            services.AddTransient<IOrderingRepository, OrderingRepository>();
            #region RabbitMQ Dependencies

            services.AddSingleton<EventBusRabbitMQConsumer>();
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory {Uri = new Uri("amqp://guest:guest@localhost:5672/")};
                return new RabbitMQConnection(factory);
            });
            

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            // hostApplicationLifetime.ApplicationStopping.Register(() =>
            // {
            //     app.ApplicationServices.GetRequiredService<EventBusRabbitMQConsumer>();
            // });

            app.UseRabbitListener();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
