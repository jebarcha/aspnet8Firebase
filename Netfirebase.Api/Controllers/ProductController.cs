using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Services.Products;

namespace Netfirebase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product request)
    {
        await _productService.Create(request);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var result = await _productService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetProduct(int id)
    {
        var result = await _productService.GetById(id);
        return Ok(result);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetProduct(string name)
    {
        var result = await _productService.GetByName(name);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] Product request)
    {
        await _productService.Update(request);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> UpdateProduct(int id)
    {
        await _productService.Delete(id);
        return Ok();
    }
}
