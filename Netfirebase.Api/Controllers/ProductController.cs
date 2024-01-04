using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netfirebase.Api.Authentication;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Models.Enums;
using Netfirebase.Api.Pagination;
using Netfirebase.Api.Services.Products;
using Netfirebase.Api.Vms;

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


    //[Authorize(Roles = "Administrator")]
    [HasPermission(PermissionEnum.WriteUser)]
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product request)
    {
        await _productService.Create(request);
        return Ok();
    }

    [Authorize]
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

    [HasPermission(PermissionEnum.WriteUser)]
    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] Product request)
    {
        await _productService.Update(request);
        return Ok();
    }

    [HasPermission(PermissionEnum.WriteUser)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(int id)
    {
        await _productService.Delete(id);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("pagination")]
    public async Task<ActionResult<PagedResults<ProductVm>>> GetPagination(
        [FromQuery] PaginationParams request
    )
    {
        var results = await _productService.GetPagination(request);
        return Ok(results);
    }


}
