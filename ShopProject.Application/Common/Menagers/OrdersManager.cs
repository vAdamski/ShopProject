using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Enums;

namespace ShopProject.Application.Common.Menagers;

public class OrdersManager : IOrdersManager
{
    private readonly ICreateOrderService _createOrderService;
    private readonly IUpdateOrderStatusService _updateOrderStatusService;

    public OrdersManager(ICreateOrderService createOrderService,
        IUpdateOrderStatusService updateOrderStatusService)
    {
        _createOrderService = createOrderService;
        _updateOrderStatusService = updateOrderStatusService;
    }
    
    public async Task<Guid> CreateOrderAsync(string userEmail, List<Product> products)
    {
        return await _createOrderService.CreateOrderAsync(userEmail, products);
    }
    
    public async Task UpdateOrderStatusAsync(Guid orderId, OrderState orderState)
    {
        await _updateOrderStatusService.UpdateOrderStatusAsync(orderId, orderState);
    }
}