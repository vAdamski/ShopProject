using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Products.Commands.AddImageToProduct;

public class AddImageToProductCommandHandler : IRequestHandler<AddImageToProductCommand, Guid>
{
    private readonly IAppDbContext _ctx;
    private readonly IProductFileManagementService _productFileManagementService;
    private readonly ILogger<AddImageToProductCommandHandler> _logger;

    public AddImageToProductCommandHandler(IAppDbContext ctx,
        IProductFileManagementService productFileManagementService,
        ILogger<AddImageToProductCommandHandler> logger)
    {
        _ctx = ctx;
        _productFileManagementService = productFileManagementService;
        _logger = logger;
    }
    
    public async Task<Guid> Handle(AddImageToProductCommand request, CancellationToken cancellationToken)
    {
        Product product;
        
        try
        {
            product = await GetProductById(request.AddImageToProductHandler.ProductId);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new Exception(e.Message);
        }

        FileData fileData = new FileData(request.AddImageToProductHandler.File);
        
        var path = await _productFileManagementService.SaveFile(fileData, request.AddImageToProductHandler.ProductId);
        
        var productImage = new ProductImage
        {
            ProductId = product.Id, 
            ImagePath= path,
            IsMain = false
        };
        
        
        await _ctx.ProductImages.AddAsync(productImage, cancellationToken);
        
        await _ctx.SaveChangesAsync(cancellationToken);
        
        return productImage.Id;
    }
    
    private async Task<Product> GetProductById(Guid id)
    {
        var product = await _ctx.Products
            .Include(x => x.ProductImages)
            .FirstAsync(x => x.Id == id);
        
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }
}