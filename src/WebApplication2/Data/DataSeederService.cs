using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.API.Data.Interfaces;

namespace WebApplication1.API.Data
{
    public class DataSeederService:IDataSeeder
    {
        private readonly OrderingContext _dbContext;
        public DataSeederService(OrderingContext context)
        {
            _dbContext = context;
        }
        public async Task SeedsData()
        {
            await ProductsData();
        }
        private async Task ProductsData()
        {
            if (await _dbContext.Orders.CountAsync() == 0)
            {
                _dbContext.Orders.AddRange(ProductsDataList);
            }

            await _dbContext.SaveChangesAsync();
        }

        private List<Order> ProductsDataList =>
            new List<Order>()
            {
                new Order()
                {
                    UserName = "Fahad",
                    EmailAddress = "fahad@gmail.com",
                    Country = "Pakistan"
                },
                new Order()
                {
                    UserName = "Asad",
                    EmailAddress = "asad@gmail.com",
                    Country = "Sweden"
                },
                new Order()
                {
                    UserName = "Muzammil",
                    EmailAddress = "muzammil@gmail.com",
                    Country = "UK"
                },
                new Order()
                {
                    UserName = "Junaid",
                    EmailAddress = "junaid@gmail.com",
                    Country = "Canada"
                },
                new Order()
                {
                    UserName = "Jabbar",
                    EmailAddress = "Jabbar@gmail.com",
                    Country = "Saudi Arab"
                },
                new Order()
                {
                    UserName = "Hafeez",
                    EmailAddress = "hafeez@gmail.com",
                    Country = "China"
                }
            };
    }
}
