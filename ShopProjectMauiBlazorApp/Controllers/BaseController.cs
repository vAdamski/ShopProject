namespace ShopProjectMauiBlazorApp.Controllers;

public abstract class BaseController
{
    protected readonly HttpClient HttpClientAuthorized;
    protected readonly HttpClient HttpClientNotAuthorized;
    protected static string ApiAddress = "";
    
    public BaseController(IHttpClientFactory httpClientFactory)
    {
        HttpClientAuthorized = httpClientFactory.CreateClient("api");
        HttpClientNotAuthorized = httpClientFactory.CreateClient("ServerAPI.NoAuthenticationClient");
        ApiAddress = WebAddressHelper.Address;
    }
}