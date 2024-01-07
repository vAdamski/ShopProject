using ShopProject.Application.Common.Builders;
using ShopProject.Domain.Entities;

namespace ShopProject.Application.Common.Interfaces;

public interface ICreateStripePaymentBuilder
{
    CreateStripePaymentBuilder WithProducts(List<Product> products);
    CreateStripePaymentBuilder WithEmail(string email);
    CreateStripePaymentBuilder WithOrderId(string orderId);
    CreateStripePaymentBuilder WithPaymentMethods(List<string> paymentMethods);
    CreateStripePaymentBuilder WithDomain(string domain);
    CreateStripePaymentBuilder WithCurrency(string currency);
    Task<string> BuildAsync(CancellationToken cancellationToken = default);
}