using ShopProject.Domain.Common;

namespace ShopProject.Domain.Entities;

public class Product : AuditableEntity
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    
    public List<ProductCategory> Categories { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<ProductImage> ProductImages { get; set; } = new();
}