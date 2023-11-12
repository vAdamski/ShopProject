using Microsoft.AspNetCore.Http;

namespace ShopProject.Shared.Dtos.Products;

public class EditableProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public List<ProductCategoryDto> Categories { get; set; } = new();
    public List<string> SelectedCategories { get; set; } = new();
    public List<ProductImageDto> Images { get; set; } = new();
}