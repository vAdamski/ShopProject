using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Payments.Commands.CreateRepaymentRequest;

public class CreateRepaymentRequestCommandHandler : IRequestHandler<CreateRepaymentRequestCommand, CreatePaymentStatus>
{
    private readonly IAppDbContext _context;
    private readonly IPaymentService _paymentService;

    public CreateRepaymentRequestCommandHandler(IAppDbContext context,
        IPaymentService paymentService)
    {
        _context = context;
        _paymentService = paymentService;
    }

    public async Task<CreatePaymentStatus> Handle(CreateRepaymentRequestCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Include(x => x.Products)
            .Where(x => x.Id == request.OrderId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (order == null)
        {
            return new CreatePaymentStatus()
            {
                Success = false,
                ErrorMessage = "Order not found"
            };
        }

        var items = order.Products;
        
        return await _paymentService.CreatePaymentAsync(items, order.UserEmail, order.Id.ToString(), cancellationToken);
    }
}