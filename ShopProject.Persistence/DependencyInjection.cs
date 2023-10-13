using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionStringDbContext.GetConnectionString()));
        services.AddScoped<IAppDbContext, AppDbContext>();
        
        return services;
    }
}