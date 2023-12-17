using OnlineShop.Aggregator.DTOs;
using OnlineShop.Aggregator.Extensions;

namespace OnlineShop.Aggregator.Services;

public class CatalogService(HttpClient _client) : ICatalogService
{
    public async Task<IEnumerable<CatalogDTO>> GetCatalog()
    {
        var response = await _client.GetAsync("/api/v1/Product/GetAll");
        return await response.ReadContentAs<List<CatalogDTO>>();
    }

    public async Task<CatalogDTO> GetCatalog(string id)
    {
        var response = await _client.GetAsync($"/api/v1/Product/GetById/{id}");
        return await response.ReadContentAs<CatalogDTO>();
    }

    public async Task<IEnumerable<CatalogDTO>> GetCatalogByCategory(string category)
    {
        var response = await _client.GetAsync($"/api/v1/Product/GetByCategory/{category}");
        return await response.ReadContentAs<List<CatalogDTO>>();
    }
}
