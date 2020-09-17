using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.API.Entities;

namespace WebApplication2.API.Repositories.Interfaces
{
    public interface IOrderingRepository
    {
        Task<IEnumerable<Order>> GetProducts();
        Task<Order> GetProduct(string id);
        Task<IEnumerable<Order>> GetProductByName(string name);
        Task<IEnumerable<Order>> GetProductByCategory(string categoryName);
                      
        Task Create(Order product);
        Task<bool> Update(Order product);
        Task<bool> Delete(string id);
    }
}
