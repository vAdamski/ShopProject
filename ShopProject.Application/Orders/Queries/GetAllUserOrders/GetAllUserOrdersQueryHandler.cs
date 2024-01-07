using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos.OrderDtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Orders.Queries.GetAllUserOrders;

public class GetAllUserOrdersQueryHandler : IRequestHandler<GetAllUserOrdersQuery, OrdersListVm>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetAllUserOrdersQueryHandler(IAppDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<OrdersListVm> Handle(GetAllUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var userEmail = _currentUserService.Email;
        
        if (userEmail is null)
            throw new NullReferenceException("User email is null");

        var orders = await _context
            .Orders
            .Where(x => x.UserEmail == userEmail)
            .Include(x => x.Products)
            .ToListAsync();
        
        var ordersListVm = new OrdersListVm();
        
        foreach (var order in orders)
        {
            var orderDto = new OrderDto
            {
                OrderId = order.Id,
                State = order.OrderState,
                OrderItems = order.Products
                    .Select(x => new OrderItem
                {
                    Name = x.ProductName,
                    Price = x.ProductPrice
                }).ToList()
            };
            
            ordersListVm.OrderDtos.Add(orderDto);
        }
        
        return ordersListVm;
    }
}