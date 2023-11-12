namespace ShopProject.Shared.Dtos;

public class ProductCategoryDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public bool IsSelected { get; set; } = false;
}