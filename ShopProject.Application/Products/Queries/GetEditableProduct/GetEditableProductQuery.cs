using MediatR;
using ShopProject.Shared.Dtos.Products;

namespace ShopProject.Application.Products.Queries.GetEditableProduct;

public class GetEditableProductQuery : IRequest<EditableProductDto>
{
    public Guid Id { get; private set; }
    
    public GetEditableProductQuery(Guid id)
    {
        Id = id;
    }
    
    public GetEditableProductQuery(string id)
    {
        Id = Guid.Parse(id);
    }
}