using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;

namespace ShopProject.Application.Products.Commands.EditProduct;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Guid>
{
    private readonly IAppDbContext _appDbContext;
    private readonly ILogger<EditProductCommandHandler> _logger;

    public EditProductCommandHandler(IAppDbContext appDbContext,
        ILogger<EditProductCommandHandler> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    
    public async Task<Guid> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        Product product;
        
        try
        {
            product = await GetProductById(request.EditProductDtoHandler.ProductId);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
        
        product.ProductName = request.EditProductDtoHandler.ProductName;
        product.ProductDescription = request.EditProductDtoHandler.ProductDescription;
        product.ProductPrice = request.EditProductDtoHandler.ProductPrice;
        product.Categories = await GetProductCategories(request.EditProductDtoHandler.Categories);

        try
        {
            var images = await _appDbContext.ProductImages
                .Where(x => x.ProductId == product.Id)
                .ToListAsync(cancellationToken);
            
            images.ForEach(x => x.IsMain = false);
            var image = images.First(x => x.Id == request.EditProductDtoHandler.MainImageId);
            
            image.IsMain = true;
            
            _appDbContext.ProductImages.UpdateRange(images);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        _appDbContext.Products.Update(product);
        
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
    
    private async Task<Product> GetProductById(Guid id)
    {
        var product = await _appDbContext.Products
            .Include(x => x.Categories)
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (product == null)
            throw new Exception("Product not found");
        
        return product;
    }

    private async Task<List<ProductCategory>> GetProductCategories(List<Guid> ids)
    {
        return await _appDbContext.ProductCategories.Where(pc => ids.Contains(pc.Id)).ToListAsync();
    }
}