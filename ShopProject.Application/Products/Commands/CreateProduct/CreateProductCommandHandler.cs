using MediatR;
using Microsoft.AspNetCore.Http;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IAppDbContext _ctx;
    private readonly IProductFileManagementService _productFileManagementService;

    public CreateProductCommandHandler(
        IAppDbContext ctx,
        IProductFileManagementService productFileManagementService)
    {
        _ctx = ctx;
        _productFileManagementService = productFileManagementService;
    }
    
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            ProductName = request.CreateProductDto.ProductName,
            ProductDescription = request.CreateProductDto.ProductDescription,
            ProductPrice = request.CreateProductDto.ProductPrice,
            Categories = request.CreateProductDto.Categories.Select(x => new ProductCategory { CategoryName = x }).ToList()
        };

        _ctx.Products.Add(product);
        await _ctx.SaveChangesAsync(cancellationToken);
        
        var productImages = await SaveImages(request.CreateProductDto.Files, product.Id);
        
        await _ctx.ProductImages.AddRangeAsync(productImages, cancellationToken);
        
        await _ctx.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }
    
    private async Task<List<ProductImage>> SaveImages(List<IFormFile> files, Guid productId)
    {
        var images = new List<ProductImage>();

        foreach (var file in files)
        {
            var filePath = await SaveFile(file, productId);
            
            if (!string.IsNullOrEmpty(filePath))
            {
                var image = new ProductImage
                {
                    ImagePath = filePath,
                    ProductId = productId
                };
                
                images.Add(image);
            }
        }
        
        return images;
    }

    private async Task<string> SaveFile(IFormFile file, Guid productId)
    {
        string filePath = string.Empty;
        
        try
        {
            filePath = await _productFileManagementService.SaveFile(new FileData(file), productId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return filePath;
    }
}