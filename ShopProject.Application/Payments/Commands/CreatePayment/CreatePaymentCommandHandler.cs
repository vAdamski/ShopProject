using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;
using Stripe.Checkout;

namespace ShopProject.Application.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentStatus>
{
    private readonly IAppDbContext _ctx;
    private readonly ICreateOrderService _createOrderService;

    public CreatePaymentCommandHandler(IAppDbContext ctx, ICreateOrderService createOrderService)
    {
        _ctx = ctx;
        _createOrderService = createOrderService;
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
        
        var orderId = await _createOrderService.CreateOrderAsync(vm.Email, items);
        
        var domain = "https://localhost:7001";
        var options = new SessionCreateOptions
        {
            SuccessUrl = domain + $"/PaymentResult/success/{orderId}",
            CancelUrl = domain + $"/PaymentResult/cancel/{orderId}",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            CustomerEmail = vm.Email,
            PaymentMethodTypes = new List<string>
            {
                "card",
                "blik"
            },
        };
        

        var itemsInfo = items.Select(x => new CartItemExtendedDto
        {
            ProductName = x.ProductName,
            ProductDescription = x.ProductDescription,
            ProductPrice = x.ProductPrice,
            Quantity = vm.Items.First(y => y.Id == x.Id).Quantity
        }).ToList();
        
        
        foreach (var item in itemsInfo)
        {
            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)item.ProductPrice * 100,
                    Currency = "pln",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                        Description = item.ProductDescription,
                    },
                },
                Quantity = item.Quantity,
            });
        }
        
        var service = new SessionService();
        Session session = await service.CreateAsync(options, cancellationToken: cancellationToken);

        return new CreatePaymentStatus()
        {
            Success = true,
            Uri = session.Url
        };
    }
}