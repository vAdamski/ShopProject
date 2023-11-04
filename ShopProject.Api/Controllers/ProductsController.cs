using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Products.Commands.CreateProduct;
using ShopProject.Application.Products.Queries.GetEditableProduct;
using ShopProject.Application.Products.Queries.GetEditableProductList;
using ShopProject.Application.Products.Queries.GetProductsPage;
using ShopProject.Shared.Dtos;

namespace ShopProject.Api.Controllers;

[Route("api/products")]
public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("get-products")]
    public async Task<IActionResult> GetProducts(int pageNo = 1, int pageSize = 10)
    {
        var result = await Mediator.Send(new GetProductsPageQuery(pageNo, pageSize));
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-editable-products")]
    public async Task<IActionResult> GetEditableProducts()
    {
        var result = await Mediator.Send(new GetEditableProductListQuery());
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-editable-product/{id}")]
    public async Task<IActionResult> GetEditableProduct(string id)
    {
        try
        {
            var result = await Mediator.Send(new GetEditableProductQuery(id));
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return NotFound();
    }
    
    [HttpPost]
    [Route("create-product")]
    public async Task<IActionResult> CreateProduct([FromForm]CreateProductDto dto)
    {
        var result = await Mediator.Send(new CreateProductCommand { CreateProductDto = dto });
        
        return Ok(result);
    }
}