using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Interfaces;

public interface IProductFileManagementService
{
    Task<string> SaveFile(FileData fileData, Guid productId);
    Task<FileData> GetFile(string path);
    string GetProductImagePath(string path);
    void DeleteFile(string path);
}