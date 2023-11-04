namespace ShopProject.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string productImageName, Guid requestId) : base(
        $"ProductImage with Id {requestId} not found.")
    {
    }
}