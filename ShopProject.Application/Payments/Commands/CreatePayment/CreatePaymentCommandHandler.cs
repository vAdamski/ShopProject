using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;
using Stripe.Checkout;

namespace ShopProject.Application.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentStatus>
{
    private readonly IAppDbContext _ctx;
    private readonly IOrdersManager _ordersManager;
    private readonly IPaymentService _paymentService;

    public CreatePaymentCommandHandler(
        IAppDbContext ctx,
        IOrdersManager ordersManager,
        IPaymentService paymentService)
    {
        _ctx = ctx;
        _ordersManager = ordersManager;
        _paymentService = paymentService;
    }

    public async Task<CreatePaymentStatus> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var vm = request.CartPaymentVm;

        if (vm.Items.Count == 0)
        {
            return new CreatePaymentStatus()
            {
                Success = false,
                ErrorMessage = "No items in cart"
            };
        }

        if (string.IsNullOrEmpty(vm.Email))
        {
            return new CreatePaymentStatus()
            {
                Success = false,
                ErrorMessage = "Email is empty"
            };
        }

        var items = await _ctx.Products
            .Where(x => vm.Items.Select(x => x.Id).Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        var orderId = await _ordersManager.CreateOrderAsync(vm.Email, items);

        return await _paymentService.CreatePaymentAsync(items, vm.Email, orderId.ToString(), cancellationToken);
    }
}