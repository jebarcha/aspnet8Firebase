using Microsoft.EntityFrameworkCore;
using Netfirebase.Api.Data;
using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Services.Products;

public class ProductService : IProductService
{
    private readonly DatabaseContext _context;

    public ProductService(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    public async Task Create(Product product)
    {
        var result = await _context.Database.ExecuteSqlAsync(@$"
            INSERT INTO ""Products""
            (
               Name, Description, Price
            )
            VALUES
            (
                {product.Name},
                {product.Description},
                {product.Price}
            )
        ");

        if (result <= 0 )
        {
            throw new Exception("Errors adding the product");
        }
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Database.SqlQuery<Product>(@$"
            SELECT * FROM ""Products""
        ").ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        var result = await _context.Database.SqlQuery<Product>(@$"
            SELECT * FROM ""Products""
            WHERE Id={id}
        ").FirstOrDefaultAsync();

        return result is null ? null! : result;
    }

    public Task<bool> SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task Update(Product product)
    {
        throw new NotImplementedException();
    }
}
