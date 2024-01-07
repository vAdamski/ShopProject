using ShopProject.Shared.ViewModels;

namespace ShopProject.Client.ApiBrokers;

public interface IOrdersBroker
{
    Task<OrdersListVm> GetOrdersAsync();
}

public class OrdersBroker : ApiBroker, IOrdersBroker
{
    public OrdersBroker(HttpClient httpClient) : base(httpClient)
    {
    }
    
    private const string OrdersRelativeUrl = "https://localhost:6001/api/Orders";
    
    public async Task<OrdersListVm> GetOrdersAsync()
    {
        var response = await GetFromJsonAsync<OrdersListVm>($"{OrdersRelativeUrl}/get-orders");
        return response;
    }
}