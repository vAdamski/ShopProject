using ShopProject.Domain.Entities;

namespace ShopProject.Application.Common.Interfaces;

public interface ICreateOrderService
{
    Task<Guid> CreateOrderAsync(string userEmail, List<Product> products);
}