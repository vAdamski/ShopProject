using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using ShopProject.Shared.Interfaces;
using ShopProjectMauiBlazorApp.Auth0;
using ShopProjectMauiBlazorApp.Controllers;
using ShopProjectMauiBlazorApp.Services;

namespace ShopProjectMauiBlazorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddSingleton(new Auth0Client(Auth0ClientSettings.OwnIds()));

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
        builder.Services.AddTransient<CustomAuthorizationMessageHandler>();
        builder.Services.AddSingleton<IPreferencesStoreClone, PreferencesStoreClone>();
        builder.Services.AddScoped<ICartService, MauiCartService>();

        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
        {
            return true;
        };

        builder.Services
            .AddHttpClient("ServerAPI.NoAuthenticationClient", client => { client.BaseAddress = new Uri("myapp://"); })
            .ConfigurePrimaryHttpMessageHandler(() => clientHandler);

        builder.Services.AddHttpClient("api").AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

        builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

        builder.Services.AddControllers();
        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}