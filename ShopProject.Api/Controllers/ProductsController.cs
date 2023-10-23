using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Products.Commands.CreateProduct;
using ShopProject.Application.Products.Queries.GetProductsPage;
using ShopProject.Shared.Dtos;

namespace ShopProject.Api.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class ProductsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(int pageNo = 1, int pageSize = 10)
    {
        var result = await Mediator.Send(new GetProductsPageQuery(pageNo, pageSize));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm]CreateProductDto dto)
    {
        var result = await Mediator.Send(new CreateProductCommand { CreateProductDto = dto });
        
        return Ok(result);
    }
}