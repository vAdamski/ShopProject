namespace ShopProject.Shared.Dtos;

public class CreatePaymentStatus
{
    public string Uri { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}