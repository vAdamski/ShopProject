namespace ShopProject.Application.Common.Interfaces;

public interface IFileStore
{
    string SafeWriteFile(byte[] content, string sourceFileName, string path);
}