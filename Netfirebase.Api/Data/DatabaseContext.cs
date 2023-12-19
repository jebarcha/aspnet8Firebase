using Microsoft.EntityFrameworkCore;
using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
}
