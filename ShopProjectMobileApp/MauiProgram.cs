using Microsoft.Extensions.Logging;
using ShopProjectMobileApp.Services;

namespace ShopProjectMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient("MyApi", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://localhost:5001/");
        });

        builder.Services.AddScoped<IProductService, ProductsService>();
        
        return builder.Build();
    }
}