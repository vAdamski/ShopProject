using ShopProject.Shared.Dtos;

namespace ShopProject.Shared.ViewModels;

public class CartPaymentVm
{
    public List<CartItemDto> Items { get; set; } = new();
    public string Email { get; set; }
}