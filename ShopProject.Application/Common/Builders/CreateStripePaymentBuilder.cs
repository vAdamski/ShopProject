using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;
using Stripe.Checkout;

namespace ShopProject.Application.Common.Builders;

public class CreateStripePaymentBuilder : ICreateStripePaymentBuilder
{
    private List<Product> _products;
    private string _email;
    private string _orderId;
    private List<string> _paymentMethods;
    private string _domain;
    private string _currency;

    public CreateStripePaymentBuilder WithProducts(List<Product> products)
    {
        _products = products;
        return this;
    }
    
    public CreateStripePaymentBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public CreateStripePaymentBuilder WithOrderId(string orderId)
    {
        _orderId = orderId;
        return this;
    }
    
    public CreateStripePaymentBuilder WithPaymentMethods(List<string> paymentMethods)
    {
        _paymentMethods = paymentMethods;
        return this;
    }
    
    public CreateStripePaymentBuilder WithDomain(string domain)
    {
        _domain = domain;
        return this;
    }
    
    public CreateStripePaymentBuilder WithCurrency(string currency)
    {
        _currency = currency;
        return this;
    }
    
    public async Task<string> BuildAsync(CancellationToken cancellationToken = default)
    {
        Validate();
        
        
        var options = new SessionCreateOptions
        {
            SuccessUrl = _domain + $"/PaymentResult/success/{_orderId}",
            CancelUrl = _domain + $"/PaymentResult/cancel/{_orderId}",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            CustomerEmail = _email,
            PaymentMethodTypes = _paymentMethods,
        };

        var itemsInfo = _products.Select(x => new CartItemExtendedDto
        {
            ProductName = x.ProductName,
            ProductDescription = x.ProductDescription,
            ProductPrice = x.ProductPrice,
            Quantity = 1
        }).ToList();
        
        foreach (var item in itemsInfo)
        {
            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)item.ProductPrice * 100,
                    Currency = _currency,
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

        return session.Url;
    }
    
    private void Validate()
    {
        if (_products == null)
            throw new ArgumentNullException(nameof(_products));

        if (string.IsNullOrEmpty(_email))
            throw new ArgumentNullException(nameof(_email));

        if (string.IsNullOrEmpty(_orderId))
            throw new ArgumentNullException(nameof(_orderId));

        if (_paymentMethods == null)
            throw new ArgumentNullException(nameof(_paymentMethods));

        if (string.IsNullOrEmpty(_domain))
            throw new ArgumentNullException(nameof(_domain));

        if (string.IsNullOrEmpty(_currency))
            throw new ArgumentNullException(nameof(_currency));
    }
}