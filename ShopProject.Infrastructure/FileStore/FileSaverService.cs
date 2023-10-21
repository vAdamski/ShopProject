using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;

namespace ShopProject.Infrastructure.FileStore;

public class FileSaverService : IFileSaverService
{
    private readonly ILogger<FileSaverService> _logger;
    private readonly IDirectoryWrapper _directoryWrapper;
    private readonly IFileWrapper _fileWrapper;

    public FileSaverService(
        ILogger<FileSaverService> logger,
        IDirectoryWrapper directoryWrapper,
        IFileWrapper fileWrapper)
    {
        _logger = logger;
        _directoryWrapper = directoryWrapper;
        _fileWrapper = fileWrapper;
    }

    public async Task<string> SaveFileAsync(FileData fileData, string pathToSave)
    {
        try
        {
            CreateFolderIfNotExist(pathToSave);
            var filePath = Path.Combine(pathToSave, fileData.FileName);
            await SaveFile(filePath, fileData.Data);

            return fileData.FileName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while saving file");
            throw;
        }
    }
    
    private void CreateFolderIfNotExist(string path)
    {
        _directoryWrapper.CreateDirectory(path);
    }

    private async Task SaveFile(string filePath, byte[] fileBytes)
    {
        await _fileWrapper.WriteAllBytesAsync(filePath, fileBytes);
    }
}