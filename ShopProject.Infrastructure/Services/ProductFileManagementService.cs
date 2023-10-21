using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;

namespace ShopProject.Infrastructure.Services;

public class ProductFileManagementService : IProductFileManagementService
{
    private readonly IStaticFilesService _staticFilesService;
    private readonly ILogger<ProductFileManagementService> _logger;

    public ProductFileManagementService(IStaticFilesService staticFilesService,
        ILogger<ProductFileManagementService> logger)
    {
        _staticFilesService = staticFilesService;
        _logger = logger;
    }

    public async Task<string> SaveFile(FileData fileData, Guid productId)
    {
        var savedPath = string.Empty;
        
        try
        {
            savedPath = await _staticFilesService.SaveFileAsync(fileData, "products", productId.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while saving file");
        }
        
        return savedPath;
    }
    
    public async Task<FileData> GetFile(string path)
    {
        try
        {
            return await _staticFilesService.GetFileAsync(path);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting file");
        }
        
        return null;
    }
    
    public void DeleteFile(string path)
    {
        try
        {
            _staticFilesService.DeleteFile(path);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting file");
        }
    }
}

