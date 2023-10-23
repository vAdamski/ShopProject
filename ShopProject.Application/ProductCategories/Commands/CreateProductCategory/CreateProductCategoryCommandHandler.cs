using MediatR;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;

namespace ShopProject.Application.Categories.Commands.CreateCategory;

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Guid>
{
    private readonly IAppDbContext _ctx;

    public CreateProductCategoryCommandHandler(IAppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Guid> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new ProductCategory()
            {
                CategoryName = request.CategoryName
            };

            await _ctx.ProductCategories.AddAsync(category, cancellationToken);

            await _ctx.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}