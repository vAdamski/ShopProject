using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Interfaces;

public interface IFileSaverService
{
    Task<string> SaveFileAsync(FileData fileData, string pathToSave);
}