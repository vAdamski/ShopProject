using ShopProject.Shared.Dtos.OrderDtos;

namespace ShopProject.Shared.ViewModels;

public class OrdersListVm
{
    public List<OrderDto> OrderDtos { get; set; } = new();
}