using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.Dtos.Products;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Products.Queries.GetEditableProductList;

public class GetEditableProductListQueryHandler : IRequestHandler<GetEditableProductListQuery, EditableProductsListViewModel>
{
    private readonly IAppDbContext _context;

    public GetEditableProductListQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<EditableProductsListViewModel> Handle(GetEditableProductListQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Where(x => x.StatusId == 1)
            .Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(x => x.Categories)
            .Include(x => x.ProductImages)
            .ToListAsync(cancellationToken);

        var productsCount = await _context.Products.CountAsync(x => x.StatusId == 1, cancellationToken);

        var editableProducts = new List<EditableProductDto>();

        foreach (var product in products)
        {
            var editableProductDto = await CreateEditableProductDto(product);
            editableProducts.Add(editableProductDto);
        }

        var viewModel = new EditableProductsListViewModel
        {
            PageSize = request.PageSize,
            CurrentPageNo = request.PageNo,
            MaxPageNo = (int)Math.Ceiling((double)productsCount / request.PageSize),
            Products = editableProducts
        };

        return viewModel;
    }

    private async Task<EditableProductDto> CreateEditableProductDto(Product product)
    {
        EditableProductDto editableProductDto = new EditableProductDto
        {
            ProductId = product.Id,
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            ProductPrice = product.ProductPrice,
            Categories = product.Categories
                .Select(x => new ProductCategoryDto { CategoryName = x.CategoryName, Id = x.Id })
                .ToList(),
            Images = await GetImagesForProduct(product.ProductImages)
        };

        return editableProductDto;
    }

    private async Task<List<ProductImageDto>> GetImagesForProduct(List<ProductImage> productImages)
    {
        var images = new List<ProductImageDto>();
        foreach (var productImage in productImages)
        {
            images.Add(new ProductImageDto
            {
                Id = productImage.Id,
                IsMain = productImage.IsMain,
            });
        }

        return images;
    }
}