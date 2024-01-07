using ShopProject.Shared.Enums;

namespace ShopProject.Application.Common.Interfaces;

public interface IUpdateOrderStatusService
{
    Task UpdateOrderStatusAsync(Guid orderId, OrderState orderState);
}