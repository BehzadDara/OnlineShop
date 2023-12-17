using OnlineShop.Aggregator.DTOs;

namespace OnlineShop.Aggregator.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogDTO>> GetCatalog();
    Task<CatalogDTO> GetCatalog(string id);
    Task<IEnumerable<CatalogDTO>> GetCatalogByCategory(string category);
}
