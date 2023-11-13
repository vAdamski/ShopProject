using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Products.Queries.GetProductsPage;

public class GetProductsPageQueryHandler : IRequestHandler<GetProductsPageQuery, ProductsViewModel>
{
    private readonly IAppDbContext _context;

    public GetProductsPageQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductsViewModel> Handle(GetProductsPageQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.Where(x => x.StatusId == 1)
            .Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(x => x.Categories)
            .Include(x => x.ProductImages)
            .ToListAsync(cancellationToken);
        
        var productsCount = await _context.Products.CountAsync(x => x.StatusId == 1, cancellationToken);
        
        var productsViewModel = new ProductsViewModel
        {
            PageSize = request.PageSize,
            CurrentPageNo = request.PageNo,
            MaxPageNo = (int)Math.Ceiling((double)productsCount / request.PageSize),
            Products = products.Select(x => new ProductMinimumInfoDto
            {
                ProductId = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                Categories = x.Categories.Select(x => x.CategoryName).ToList(),
                ImageId = GetMainImageOrDefault(x).ToString()
            }).ToList()
        };

        return productsViewModel;
    }
    
    private Guid GetMainImageOrDefault(Product product)
    {
        if (!product.ProductImages.Any())
        {
            return Guid.Empty;
        }

        return product.ProductImages.FirstOrDefault(x => x.IsMain)?.Id ?? product.ProductImages.First().Id;
    }
}