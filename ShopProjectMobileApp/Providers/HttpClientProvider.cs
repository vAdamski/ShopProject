using ShopProjectMobileApp.Services;

namespace ShopProjectMobileApp.Providers;

public static class HttpClientProvider
{
    public static HttpClient GetHttpClient()
    {
        HttpClient httpClient;
#if DEBUG
        HttpsClientHandlerService handler = new HttpsClientHandlerService();
        httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            httpClient = new HttpClient();
#endif
        return httpClient;
    }
}