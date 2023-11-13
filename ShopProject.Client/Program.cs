using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopProject.Client;
using ShopProject.Client.ApiBrokers;
using ShopProject.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<IProductCategoriesApi, ProductCategoriesApi>();
builder.Services.AddTransient<IProductApi, ProductApi>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient("ServerAPI.NoAuthenticationClient",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


builder.Services.AddHttpClient("api").AddHttpMessageHandler(sp =>
{
    var handler = sp.GetService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { "https://localhost:6001/", "https://localhost:5001/" },
            scopes: new[] { "api1", "IdentityServerApi" });

    return handler;
});

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddOidcAuthentication(options => { builder.Configuration.Bind("oidc", options.ProviderOptions); });

await builder.Build().RunAsync();