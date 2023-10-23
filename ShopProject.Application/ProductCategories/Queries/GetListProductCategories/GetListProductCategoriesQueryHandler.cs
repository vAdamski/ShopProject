using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.ProductCategories.Queries.GetListProductCategories;

public class GetListProductCategoriesQueryHandler : IRequestHandler<GetListProductCategoriesQuery, ProductCategoryListViewModel>
{
    private readonly IAppDbContext _ctx;

    public GetListProductCategoriesQueryHandler(IAppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<ProductCategoryListViewModel> Handle(GetListProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _ctx.ProductCategories
                .Select(x => new ProductCategoryDto()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName
                })
                .ToListAsync(cancellationToken);


            return new ProductCategoryListViewModel()
            {
                ProductCategories = categories
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}