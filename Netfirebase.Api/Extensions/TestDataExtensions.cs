using Bogus;
using Netfirebase.Api.Data;
using Netfirebase.Api.Models.Domain;
using System.Runtime.CompilerServices;

namespace Netfirebase.Api.Extensions;

public static class TestDataExtensions
{
    public static async void AddTestData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider;
        var dbContext = service.GetRequiredService<DatabaseContext>();

        if (!dbContext.Products.Any())
        {
            var productCollection = new List<Product>();
            var faker = new Faker();
            for ( var i = 0; i<=1000; i++)
            {
                productCollection.Add(new Product
                {
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Commerce.ProductDescription(),
                    Price = faker.Random.Decimal(100,50000)
                });
            }

            await dbContext.Products.AddRangeAsync(productCollection);
            await dbContext.SaveChangesAsync();
        }


    }

}
