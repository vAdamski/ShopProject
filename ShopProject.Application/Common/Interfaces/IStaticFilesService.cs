using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Interfaces;

public interface IStaticFilesService
{
    Task<FileData> GetFileAsync(string path);
    Task<string> SaveFileAsync(FileData fileData, params string[] folderNames);
    void DeleteFile(string path);
}