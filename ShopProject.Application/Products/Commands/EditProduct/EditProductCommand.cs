using MediatR;
using ShopProject.Shared.Dtos.Products;

namespace ShopProject.Application.Products.Commands.EditProduct;

public class EditProductCommand : IRequest<Guid>
{
    public EditProductDtoHandler EditProductDtoHandler { get; set; }
}