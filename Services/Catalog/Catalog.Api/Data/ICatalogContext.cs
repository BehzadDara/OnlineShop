using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}
