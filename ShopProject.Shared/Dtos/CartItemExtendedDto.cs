namespace ShopProject.Shared.Dtos;

public class CartItemExtendedDto
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}