using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Pagination;
using Netfirebase.Api.Vms;

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

    Task<PagedResults<ProductVm>> GetPagination(PaginationParams request);
}
