using MediatR;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Products.Commands.AddImageToProduct;

public class AddImageToProductCommand : IRequest<Guid>
{
    public AddImageToProductHandler AddImageToProductHandler { get; set; }
}