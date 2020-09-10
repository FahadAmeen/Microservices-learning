using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.Data.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedsData();
    }
}
