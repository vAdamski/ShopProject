using MediatR;

namespace ShopProject.Application.Categories.Commands.CreateCategory;

public class CreateProductCategoryCommand : IRequest<Guid>
{
    public string CategoryName { get; set; } = "";
}