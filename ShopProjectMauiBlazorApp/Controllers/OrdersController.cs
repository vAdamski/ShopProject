using System.Net.Http.Json;
using ShopProject.Shared.ViewModels;

namespace ShopProjectMauiBlazorApp.Controllers;

public interface IOrdersController
{
    Task<OrdersListVm> GetOrdersAsync();
}

public class OrdersController(IHttpClientFactory httpClientFactory)
    : BaseController(httpClientFactory), IOrdersController
{
    private readonly string _controller = $"{ApiAddress}/api/Orders";

    public async Task<OrdersListVm> GetOrdersAsync()
    {
        try
        {
            return await HttpClientAuthorized.GetFromJsonAsync<OrdersListVm>($"{_controller}/get-orders");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}