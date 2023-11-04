using MediatR;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Files.Queries.GetImage;

public class GetImageQuery : IRequest<FileData>
{
    public Guid Id { get; private set; }
    
    public GetImageQuery(Guid id)
    {
        Id = id;
    }
    
    public GetImageQuery(string id)
    {
        Id = Guid.Parse(id);
    }
}