using ShopProject.Domain.Common;
using ShopProject.Shared.Enums;

namespace ShopProject.Domain.Entities;

public class Order : AuditableEntity
{
    public string UserEmail { get; set; }
    public OrderState OrderState { get; set; }
    public List<Product> Products { get; set; } = new();
    public decimal Amount { get; set; }
}