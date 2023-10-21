using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Infrastructure.FileStore;

public class DirectoryWrapper : IDirectoryWrapper
{
    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }
}