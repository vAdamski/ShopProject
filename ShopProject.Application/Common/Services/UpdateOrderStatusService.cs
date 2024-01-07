using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.Enums;

namespace ShopProject.Application.Common.Services;

public class UpdateOrderStatusService : IUpdateOrderStatusService
{
    private readonly IAppDbContext _context;
    private readonly ICodesMailSenderService _codesMailSenderService;

    public UpdateOrderStatusService(IAppDbContext context, ICodesMailSenderService codesMailSenderService)
    {
        _context = context;
        _codesMailSenderService = codesMailSenderService;
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderState orderState)
    {
        Order order = await GetOrderAsync(orderId);
        
        await UpdateOrderStateAsync(order, orderState);
        
        await SendMailWithCodesAsync(order);
    }
    
    private async Task<Order> GetOrderAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == orderId);
        
        if (order == null)
            throw new Exception("Order not found");
        
        return order;
    }

    private async Task UpdateOrderStateAsync(Order order, OrderState orderState)
    {
        order.OrderState = orderState;

        await _context.SaveChangesAsync("System");
    }
    
    private CodesMailRequest CreateCodesMailRequest(Order order)
    {
        return new CodesMailRequest(
            order.UserEmail,
            order.Id.ToString(),
            order.Products.Select(x => x.ProductName).ToList());
    }
    
    private async Task SendMailWithCodesAsync(Order order)
    {
        CodesMailRequest codesMailRequest = CreateCodesMailRequest(order);

        await _codesMailSenderService.SendMail(codesMailRequest);
    }
}