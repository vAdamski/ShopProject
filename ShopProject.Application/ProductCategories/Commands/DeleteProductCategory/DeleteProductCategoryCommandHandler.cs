using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Exceptions;

namespace ShopProject.Application.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteProductCategoryCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _appDbContext
            .ProductCategories
            .FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId, cancellationToken);
        
        if (productCategory == null)
        {
            throw new NotFoundException(nameof(productCategory), request.ProductCategoryId);
        }
        
        var products = await _appDbContext
            .Products
            .Include(x => x.Categories)
            .Where(x => x.Categories.Any(x => x.Id == request.ProductCategoryId))
            .ToListAsync(cancellationToken);
        
        foreach (var product in products)
        {
            product.Categories.Remove(productCategory);
        }
        
        _appDbContext.Products.UpdateRange(products);
        
        _appDbContext.ProductCategories.Remove(productCategory);
        
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}