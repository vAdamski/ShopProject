using MediatR;

namespace ShopProject.Application.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest
{
    public Guid ProductCategoryId { get; set; }
}