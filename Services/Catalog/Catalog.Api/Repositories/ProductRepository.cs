using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class ProductRepository(ICatalogContext _catalogContext) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _catalogContext.Products.Find(x => true).ToListAsync();
    }
    public async Task<Product> GetById(string id)
    {
        return await _catalogContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Product> GetByName(string name)
    {
        return await _catalogContext.Products.Find(x => x.Name == name).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Product>> GetByCategory(string category)
    {
        return await _catalogContext.Products.Find(x => x.Category == category).ToListAsync();
    }
    public async Task Create(Product product)
    {
        await _catalogContext.Products.InsertOneAsync(product);
    }
    public async Task<bool> Update(Product product)
    {
        var result = await _catalogContext.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    public async Task<bool> Delete(string id)
    {
        var result = await _catalogContext.Products.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
} 
