using Microsoft.AspNetCore.Http;

namespace ShopProject.Shared.Dtos;

public class ProductMinimumInfoDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public List<string> Categories { get; set; }
    public IFormFile Image { get; set; }
}