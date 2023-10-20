using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Products.Queries.GetProductsPage;

namespace ShopProject.Api.Controllers;

public class ProductsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(int pageNo = 1, int pageSize = 10)
    {
        var result = await Mediator.Send(new GetProductsPageQuery(pageNo, pageSize));
        return Ok(result);
    }
}