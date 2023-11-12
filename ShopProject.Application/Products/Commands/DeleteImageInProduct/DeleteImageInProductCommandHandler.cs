using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;

namespace ShopProject.Application.Products.Commands.DeleteImageInProduct;

public class DeleteImageInProductCommandHandler : IRequestHandler<DeleteImageInProductCommand>
{
    private readonly IAppDbContext _ctx;
    private readonly ILogger<DeleteImageInProductCommandHandler> _logger;

    public DeleteImageInProductCommandHandler(IAppDbContext ctx, ILogger<DeleteImageInProductCommandHandler> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }
    
    public async Task Handle(DeleteImageInProductCommand request, CancellationToken cancellationToken)
    {
        Product product;
        
        try
        {
            product = await GetProductById(request.ProductId);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new Exception(e.Message);
        }
        
        var imageToDelete = product.ProductImages.FirstOrDefault(x => x.Id == request.ImageId);
        
        if (imageToDelete == null)
        {
            throw new Exception("Image not found");
        }
        
        if (imageToDelete.IsMain && product.ProductImages.Count > 0)
        {
            var defaultImage = product.ProductImages
                .First(x => x.Id != imageToDelete.Id);
            defaultImage.IsMain = true;
        }
        
        product.ProductImages.Remove(imageToDelete);
        
        _ctx.Products.Update(product);
        
        await _ctx.SaveChangesAsync(cancellationToken);
    }

    private async Task<Product> GetProductById(Guid productId)
    {
        var product = await _ctx.Products
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == productId);
        
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }
}