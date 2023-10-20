using ShopProject.Domain.Common;
using ShopProject.Shared.Enums;

namespace ShopProject.Domain.Entities;

public class Order : AuditableEntity
{
    public OrderState OrderState { get; set; }
    public bool IsPaid { get; set; }

    public string Country { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }

    public string UserEmail { get; set; }
    public string PhoneNumber { get; set; }
    
    public List<Product> Products { get; set; } = new();
}