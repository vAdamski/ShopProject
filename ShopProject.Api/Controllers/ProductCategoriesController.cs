using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Categories.Commands.CreateCategory;
using ShopProject.Application.ProductCategories.Commands.DeleteProductCategory;
using ShopProject.Application.ProductCategories.Queries.GetListProductCategories;
using ShopProject.Application.Products.Commands.CreateProduct;
using ShopProject.Shared.Dtos;
namespace ShopProject.Api.Controllers;

[Route("api/product-categories")]
public class ProductCategoriesController : BaseController
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetProductCategories()
    {
        var result = await Mediator.Send(new GetListProductCategoriesQuery());
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProductCategory(CreateProductCategoryDto createProductCategoryDto)
    {
        var result = await Mediator.Send(new CreateProductCategoryCommand() { CategoryName = createProductCategoryDto.ProductCategoryName});

        return Ok(result);
    }
    
    [HttpDelete]
    [Route("delete/{productCategoryId}")]
    public async Task<IActionResult> DeleteProductCategory(Guid productCategoryId)
    {
        await Mediator.Send(new DeleteProductCategoryCommand() { ProductCategoryId = productCategoryId});

        return NoContent();
    }
}