using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Interfaces
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
