namespace ShopProject.Shared.Dtos;

public class CodesMailRequest
{
    public string OrderId { get; }
    public string CustomerEmail { get; }
    public List<string> Games { get; }

    public CodesMailRequest(string customerEmail,
        string orderId,
        List<string> games)
    {
        CustomerEmail = customerEmail;
        OrderId = orderId;
        Games = games;
    }
}