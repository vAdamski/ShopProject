using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;

namespace ShopProject.Infrastructure.FileStore;

public class DiscFileDownloadService : IDiscFileDownloadService
{
    private readonly ILogger<DiscFileDownloadService> _logger;

    public DiscFileDownloadService(ILogger<DiscFileDownloadService> logger)
    {
        _logger = logger;
    }
    
    public async Task<FileData> GetFileAsync(string path)
    {
        try
        {
            var data = await File.ReadAllBytesAsync(path);
            var fileName = Path.GetFileName(path);
        
            return new FileData(data, fileName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting file");
            throw;
        }
    }
}