using Catalog.API.Entities;

namespace Catalog.API.Data.Interfaces
{
    public interface ICatalogContext
    {
        Product Products { get; }
    }
}
