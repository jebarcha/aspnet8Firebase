using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Services.Products;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int id);
    Task<List<Product>> GetByName(string name);
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
