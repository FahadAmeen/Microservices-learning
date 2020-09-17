using WebApplication1.API.Entities;

namespace WebApplication1.API.Data.Interfaces
{
    public interface ICatalogContext
    {
        Product Products { get; }
    }
}
