namespace ShopProject.Shared.Dtos.Products;

public class EditProductDtoHandler
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; } = 0;
    public Guid MainImageId { get; set; } = Guid.Empty;
    public List<Guid> Categories { get; set; } = new List<Guid>();
}