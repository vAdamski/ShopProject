using Microsoft.Extensions.DependencyInjection;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Infrastructure.Common;
using ShopProject.Infrastructure.FileStore;
using ShopProject.Infrastructure.Services;

namespace ShopProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        
        // File store
        services.AddTransient<IProductFileManagementService, ProductFileManagementService>();
        services.AddTransient<IDirectoryWrapper, DirectoryWrapper>();
        services.AddTransient<IDiscFileDownloadService, DiscFileDownloadService>();
        services.AddTransient<IFileSaverService, FileSaverService>();
        services.AddTransient<IFileStore, FileStore.FileStore>();
        services.AddTransient<IFileWrapper, FileWrapper>();
        services.AddTransient<IStaticFilesService, StaticFilesService>();
        services.AddSingleton<IStaticFilePath, StaticFilePath>();
        
        
        return services;
    }
}