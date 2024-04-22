using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using ShopProjectMauiBlazorApp.Auth0;

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
        
        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = "dev-u8i8ssxpwy2t06gm.us.auth0.com",
            ClientId = "Vxa9egkZGTQzdvYhmxL5Bxhjp9QbUPlk",
            Scope = "openid",
            RedirectUri = "myapp://callback",
        }));
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}