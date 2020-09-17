using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
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
using WebApplication1.API.Data;
using WebApplication1.API.Data.Interfaces;
using WebApplication1.API.Repositories;
using WebApplication1.API.Repositories.Interfaces;

namespace WebApplication1.API
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
            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ServerConnection")));
            services.AddTransient<IDataSeeder, DataSeederService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            #region RabbitMQ Dependencies

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory=new ConnectionFactory();
                factory.Uri = new Uri("amqp://guest:guest@localhost:5672/");
                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
