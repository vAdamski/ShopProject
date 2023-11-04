using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.Dtos.Products;

namespace ShopProject.Application.Products.Queries.GetEditableProduct;

public class GetEditableProductQueryHandler : IRequestHandler<GetEditableProductQuery, EditableProductDto>
{
    private readonly IAppDbContext _ctx;

    public GetEditableProductQueryHandler(IAppDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<EditableProductDto> Handle(GetEditableProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _ctx.Products
            .Include(x => x.Categories)
            .Include(x => x.ProductImages)
            .Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        return BuildProductCategoryDto(product);
    }
    
    private EditableProductDto BuildProductCategoryDto(Product product)
    {
        var productDto = new EditableProductDto();
        
        productDto.ProductId = product.Id;
        productDto.ProductName = product.ProductName;
        productDto.ProductDescription = product.ProductDescription;
        productDto.ProductPrice = product.ProductPrice;
        productDto.Categories = product.Categories.Select(x => new ProductCategoryDto
        {
            Id = x.Id,
            CategoryName = x.CategoryName
        }).ToList();
        productDto.Images = product.ProductImages.Select(x => new ProductImageDto
        {
            Id = x.Id,
            IsMain = x.IsMain
        }).ToList();
        
        return productDto;
    }
}