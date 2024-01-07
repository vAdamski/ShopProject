using ShopProject.Shared.ViewModels;

namespace ShopProject.Client.ApiBrokers;

public interface IPaymentBroker
{
    Task<string> PostPaymentAsync(CartPaymentVm cartPaymentVm);
    Task<string> PostPaymentAsync(Guid orderId);
}

public class PaymentBroker : ApiBroker, IPaymentBroker
{
    public PaymentBroker(HttpClient httpClient) : base(httpClient)
    {
    }
    
    private const string PaymentRelativeUrl = "https://localhost:6001/api/payment";

    public async Task<string> PostPaymentAsync(CartPaymentVm cartPaymentVm)
    {
        var response = await PostAsJsonAsyncWithResponse($"{PaymentRelativeUrl}/create-checkout-session", cartPaymentVm);
        if (response.IsSuccessStatusCode)
        {
            var redirectUrl = await response.Content.ReadAsStringAsync();
            return redirectUrl;
        }
        
        return "";
    }
    
    public async Task<string> PostPaymentAsync(Guid orderId)
    {
        var response = await PostAsync($"{PaymentRelativeUrl}/create-checkout-session/{orderId}");
        
        if (response.IsSuccessStatusCode)
        {
            var redirectUrl = await response.Content.ReadAsStringAsync();
            return redirectUrl;
        }
        
        return "";
    }
}