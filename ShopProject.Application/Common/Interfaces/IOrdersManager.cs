using ShopProject.Domain.Entities;
using ShopProject.Shared.Enums;

namespace ShopProject.Application.Common.Interfaces;

public interface IOrdersManager
{
    Task<Guid> CreateOrderAsync(string userEmail, List<Product> products);
    Task UpdateOrderStatusAsync(Guid orderId, OrderState orderState);
}