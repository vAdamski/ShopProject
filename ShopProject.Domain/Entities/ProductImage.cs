using ShopProject.Domain.Common;

namespace ShopProject.Domain.Entities;

public class ProductImage : AuditableEntity
{
    public Guid ProductId { get; set; }
    public Product Prodcut { get; set; }
    public string ImagePath { get; set; }
    public bool IsMain { get; set; }
}