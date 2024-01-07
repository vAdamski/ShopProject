using MediatR;
using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Application.Orders.Commands.UpdateOrderState;

public class UpdateOrderStateCommandHandler : IRequestHandler<UpdateOrderStateCommand>
{
    private readonly IOrdersManager _ordersManager;

    public UpdateOrderStateCommandHandler(IOrdersManager ordersManager)
    {
        _ordersManager = ordersManager;
    }
    
    public async Task Handle(UpdateOrderStateCommand request, CancellationToken cancellationToken)
    {
        await _ordersManager.UpdateOrderStatusAsync(request.OrderId, request.State);
    }
}