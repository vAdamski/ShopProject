using ShopProject.Domain.Common;

namespace ShopProject.Domain.Entities;

public class ProductCategory : AuditableEntity
{
    public string CategoryName { get; set; }
    
    public List<Product> Products { get; set; } = new();
}