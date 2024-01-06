using ShopProject.Shared.Enums;

namespace ShopProject.Shared.Dtos.OrderDtos;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public OrderState State { get; set; }
    public string StatusMessage => State switch
    {
        OrderState.WaitingForPayment => "Oczekuje na płatność",
        OrderState.Paid => "Zapłacono",
        _ => "Unknown"
    };
    public List<OrderItem> OrderItems { get; set; } = new();
}