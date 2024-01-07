using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Interfaces;

public interface IPaymentService
{
    Task<CreatePaymentStatus> CreatePaymentAsync(List<Product> items, string email, string orderId,
        CancellationToken cancellationToken = default);
}