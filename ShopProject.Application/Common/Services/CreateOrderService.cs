using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Enums;

namespace ShopProject.Application.Common.Services;

public class CreateOrderService : ICreateOrderService
{
    private readonly IAppDbContext _context;

    public CreateOrderService(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> CreateOrderAsync(string userEmail, List<Product> products)
    {
        var order = new Order
        {
            UserEmail = userEmail,
            Products = products,
            Amount = products.Sum(p => p.ProductPrice),
            OrderState = OrderState.WaitingForPayment
        };
        
        await _context.Orders.AddAsync(order);
        
        await _context.SaveChangesAsync(userEmail);

        return order.Id;
    }
}

//https://localhost:7001/PaymentResult/success/22059b6f-f1f0-4229-44ed-08dc0eea48f8