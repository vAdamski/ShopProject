using MediatR;

namespace ShopProject.Application.Products.Commands.DeleteImageInProduct;

public class DeleteImageInProductCommand : IRequest
{
    public Guid ProductId { get; set; }
    public Guid ImageId { get; set; }
}