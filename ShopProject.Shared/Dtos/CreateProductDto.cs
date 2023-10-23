namespace ShopProject.Shared.Dtos;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public List<string> Categories { get; set; } = new();
    public List<ProductFileDto> Files { get; set; } = new();
}