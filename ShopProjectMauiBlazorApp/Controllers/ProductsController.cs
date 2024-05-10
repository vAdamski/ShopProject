using ShopProject.Shared.ViewModels;

namespace ShopProjectMauiBlazorApp.Controllers;

public class ProductsController(IHttpClientFactory httpClientFactory)
    : BaseController(httpClientFactory)
{
    private string _controller = "api/products";
}