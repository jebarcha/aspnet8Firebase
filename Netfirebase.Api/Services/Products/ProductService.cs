using Bogus.DataSets;
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
        try
        {
            await _context.Database.ExecuteSqlAsync(@$"
                CALL sp_insert_product({product.Price}, {product.Name}, {product.Description})
            ");
        }
        catch (Exception ex)
        {
            throw new Exception("Error inserting the product", ex);
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            await _context.Database.ExecuteSqlAsync($@"
                CALL sp_delete_product({id})
            ");
        }
        catch (Exception)
        {
            throw new Exception($"Errors deleting the product id {id}");
        }
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Database.SqlQuery<Product>(@$"
            SELECT * FROM fx_query_product_all()
        ").ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        var result = await _context.Database.SqlQuery<Product>(@$"
            SELECT * FROM fx_query_product_by_id({id})
        ").ToListAsync();

        var product = result.FirstOrDefault();
        return product;
    }

    public async Task<List<Product>> GetByName(string name)
    {
        var result = await _context.Database.SqlQuery<Product>(@$"
            SELECT * FROM fx_query_product_by_name({name})
        ").ToListAsync();

        return result is null ? null! : result;
    }

    public Task<bool> SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task Update(Product product)
    {
        try
        {
            await _context.Database.ExecuteSqlAsync(@$"
                CALL sp_update_product({product.Id}, {product.Price}, {product.Name}, {product.Description})
            ");
        }
        catch (Exception ex)
        {
            throw new Exception($"Errors updating the product id {product.Id}", ex);
        }
       
    }
}
