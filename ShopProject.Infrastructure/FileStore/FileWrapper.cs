using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Infrastructure.FileStore;

public class FileWrapper : IFileWrapper
{
    public void WriteAllBytes(string outputFile, byte[] content)
    {
        File.WriteAllBytes(outputFile, content);
    }

    public async Task WriteAllBytesAsync(string outputFile, byte[] content)
    {
        await File.WriteAllBytesAsync(outputFile, content);
    }
}