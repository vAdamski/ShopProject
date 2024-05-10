namespace ShopProjectMauiBlazorApp.Controllers;

public static class ControllersDI
{
    public static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services.AddTransient<IOrdersController, OrdersController>();

        return services;
    }
}