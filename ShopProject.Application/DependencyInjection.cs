using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Common.Services;

namespace ShopProject.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // DI
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient<ICreateOrderService, CreateOrderService>();
        
        return services;
    }
}