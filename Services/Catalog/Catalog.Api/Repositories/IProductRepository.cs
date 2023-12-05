using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(string id);
    Task<Product> GetByName(string name);
    Task<IEnumerable<Product>> GetByCategory(string category);
    Task Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(string id);
}
