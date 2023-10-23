using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MimeTypes;

namespace ShopProject.Shared.Dtos;

public class FileData
{
    public FileData(byte[] data, string fileName, string contentType)
    {
        Data = data;
        FileName = fileName;
        ContentType = contentType;
    }

    public FileData(byte[] data, string fileName)
    {
        Data = data;
        FileName = fileName;
        string fileExtension = fileName.Split('.').Last();
        ContentType = MimeTypeMap.List.MimeTypeMap.GetMimeType(fileExtension).First();
    }

    public FileData(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        Data = memoryStream.ToArray();
        FileName = file.FileName;
        ContentType = file.ContentType;
    }
    
    public FileData(IBrowserFile file)
    {
        using var memoryStream = new MemoryStream();
        file.OpenReadStream().CopyTo(memoryStream);
        Data = memoryStream.ToArray();
        FileName = file.Name;
        ContentType = file.ContentType;
    }

    public byte[] Data { get; private set; }
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
}