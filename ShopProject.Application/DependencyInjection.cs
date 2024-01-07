using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ShopProject.Application.Common.Builders;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Common.Menagers;
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
        services.AddTransient<IUpdateOrderStatusService, UpdateOrderStatusService>();
        services.AddTransient<IOrdersManager, OrdersManager>();
        services.AddTransient<ICreateStripePaymentBuilder, CreateStripePaymentBuilder>();
        services.AddTransient<IPaymentService, PaymentService>();
        
        return services;
    }
}