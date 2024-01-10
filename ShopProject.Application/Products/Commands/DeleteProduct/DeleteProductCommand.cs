using MediatR;

namespace ShopProject.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Guid ProductId { get; }

    public DeleteProductCommand(Guid productId)
    {
        ProductId = productId;
    }
}