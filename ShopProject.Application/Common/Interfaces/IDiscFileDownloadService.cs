using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Interfaces;

public interface IDiscFileDownloadService
{
    Task<FileData> GetFileAsync(string path);
}