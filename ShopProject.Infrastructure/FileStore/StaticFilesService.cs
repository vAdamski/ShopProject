using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Infrastructure.Common;
using ShopProject.Shared.Dtos;

namespace ShopProject.Infrastructure.FileStore;

public class StaticFilesService : IStaticFilesService
{
    private readonly IStaticFilePath _staticFilePath;
    private readonly IFileSaverService _fileSaverService;
    private readonly IDiscFileDownloadService _discFileDownloadService;
    private readonly ILogger<StaticFilesService> _logger;

    public StaticFilesService(IStaticFilePath staticFilePath,
        IFileSaverService fileSaverService,
        IDiscFileDownloadService discFileDownloadService,
        ILogger<StaticFilesService> logger)
    {
        _staticFilePath = staticFilePath;
        _fileSaverService = fileSaverService;
        _discFileDownloadService = discFileDownloadService;
        _logger = logger;
    }
    
    public async Task<FileData> GetFileAsync(string path)
    {
        try
        {
            var fullPath = GetFilePath(path);
        
            return await _discFileDownloadService.GetFileAsync(fullPath);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting file");
            throw;
        }
    }
    
    public string GetFilePath(string path)
    {
        try
        {
            var fullPath = Path.Combine(_staticFilePath.StaticFolderFilePath, path);
        
            return fullPath;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting file");
            throw;
        }
    }

    public async Task<string> SaveFileAsync(FileData fileData, params string[] folderNames)
    {
        try
        {
            var folderPath = Path.Combine(folderNames);
            var pathToSave = Path.Combine(_staticFilePath.StaticFolderFilePath, folderPath);
            await _fileSaverService.SaveFileAsync(fileData, pathToSave);

            return PathSlashHelper.ChangePathSlash(Path.Combine(folderPath, fileData.FileName));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while saving file");
        }
        
        return string.Empty;
    }

    public void DeleteFile(string path)
    {
        try
        {
            var fullPath = Path.Combine(_staticFilePath.StaticFolderFilePath, path);
            
            File.Delete(fullPath);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting file");
            throw;
        }
    }
}