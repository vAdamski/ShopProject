using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Enums;

namespace ShopProject.Application.Common.Services;

public class UpdateOrderStatusService : IUpdateOrderStatusService
{
    private readonly IAppDbContext _context;

    public UpdateOrderStatusService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderState orderState)
    {
        var order = await _context.Orders.FindAsync(orderId);
        
        if (order == null)
            throw new Exception("Order not found");

        order.OrderState = orderState;

        await _context.SaveChangesAsync("System");
    }
}