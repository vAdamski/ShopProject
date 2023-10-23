using MediatR;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public CreateProductDto CreateProductDto { get; set; }
}