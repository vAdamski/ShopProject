using Microsoft.AspNetCore.Http;

namespace ShopProject.Shared.Dtos;

public class AddImageToProductHandler
{
    public Guid ProductId { get; set; }
    public IFormFile File { get; set; }
}