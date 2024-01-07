using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Services;

public class PaymentService : IPaymentService
{
    private readonly ICreateStripePaymentBuilder _createStripePaymentBuilder;

    public PaymentService(ICreateStripePaymentBuilder createStripePaymentBuilder)
    {
        _createStripePaymentBuilder = createStripePaymentBuilder;
    }

    public async Task<CreatePaymentStatus> CreatePaymentAsync(List<Product> items, string email, string orderId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var url = await _createStripePaymentBuilder
                .WithProducts(items)
                .WithEmail(email)
                .WithOrderId(orderId)
                .WithPaymentMethods(new List<string>
                {
                    "card",
                    "blik"
                })
                .WithDomain("https://localhost:7001")
                .WithCurrency("pln")
                .BuildAsync(cancellationToken);

            return new CreatePaymentStatus()
            {
                Success = true,
                Uri = url
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return new CreatePaymentStatus()
            {
                Success = false,
                ErrorMessage = e.Message
            };
        }
    }
}