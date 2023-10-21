namespace ShopProject.Application.Common.Interfaces;

public interface IFileWrapper
{
    void WriteAllBytes(string outputFile, byte[] content);
    Task WriteAllBytesAsync(string outputFile, byte[] content);
}