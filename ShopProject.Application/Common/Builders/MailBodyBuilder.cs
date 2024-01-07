using System.Text;

namespace ShopProject.Application.Common.Builders;

public class MailBodyBuilder
{
    private List<string> _products;
    private string _orderId;
    
    public MailBodyBuilder(List<string> products)
    {
        if (products == null)
            throw new ArgumentNullException(nameof(products));
        
        if (products.Count == 0)
            throw new ArgumentException("Products list is empty", nameof(products));
        
        _products = products;
    }
    
    public MailBodyBuilder WithOrderId(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
            throw new ArgumentException("OrderId is empty", nameof(orderId));
        
        _orderId = orderId;
        return this;
    }
    
    public string Build()
    {
        var body = new StringBuilder();
        body.Append($"<h1>Twoje zamówienie nr: {_orderId}</h1>");
        body.Append("<ul>");
        foreach (var product in _products)
        {
            body.Append($"<li>{product}: {Guid.NewGuid()}</li>");
        }
        body.Append("</ul>");
        return body.ToString();
    }
}