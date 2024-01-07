using MediatR;
using ShopProject.Shared.Enums;

namespace ShopProject.Application.Orders.Commands.UpdateOrderState;

public class UpdateOrderStateCommand : IRequest
{
    public Guid OrderId { get; set; }
    public OrderState State { get; set; }
}