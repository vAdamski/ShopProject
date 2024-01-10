using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Products.Commands.AddImageToProduct;
using ShopProject.Application.Products.Commands.CreateProduct;
using ShopProject.Application.Products.Commands.DeleteImageInProduct;
using ShopProject.Application.Products.Commands.DeleteProduct;
using ShopProject.Application.Products.Commands.EditProduct;
using ShopProject.Application.Products.Queries.GetEditableProduct;
using ShopProject.Application.Products.Queries.GetEditableProductList;
using ShopProject.Application.Products.Queries.GetProductsPage;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.Dtos.Products;

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
    [Route("get-products/{pageNo}")]
    [Route("get-products/{pageNo}/{pageSize}")]
    public async Task<IActionResult> GetProducts(int pageNo = 1, int pageSize = 10)
    {
        var result = await Mediator.Send(new GetProductsPageQuery(pageNo, pageSize));
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-editable-products")]
    public async Task<IActionResult> GetEditableProducts()
    {
        var result = await Mediator.Send(new GetEditableProductListQuery(pageSize: Int32.MaxValue, pageNo:1));
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
    
    [HttpPost]
    [Route("edit-product")]
    public async Task<IActionResult> EditProduct([FromForm]EditProductDtoHandler dto)
    {
        var result = await Mediator.Send(new EditProductCommand { EditProductDtoHandler = dto });
        
        return Ok(result);
    }
    
    [HttpPost]
    [Route("add-image")]
    public async Task<IActionResult> AddImageToProduct([FromForm]AddImageToProductHandler image)
    {
        var result = await Mediator.Send(new AddImageToProductCommand { AddImageToProductHandler = image });
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("delete-image/{productId}/{imageId}")]
    public async Task<IActionResult> DeleteImageInProduct(string productId, string imageId)
    {
        await Mediator.Send(new DeleteImageInProductCommand { ProductId = Guid.Parse(productId), ImageId = Guid.Parse(imageId) });
        
        return NoContent();
    }
    
    [HttpDelete]
    [Route("delete-product/{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await Mediator.Send(new DeleteProductCommand(productId));
        
        return NoContent();
    }
}