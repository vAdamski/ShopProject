using MediatR;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Products.Queries.GetProductsPage;

public class GetProductsPageQuery : IRequest<ProductsViewModel>
{
    public int PageNo { get;}
    public int PageSize { get;}
    
    public GetProductsPageQuery()
    {
        PageNo = 1;
        PageSize = 10;
    }

    public GetProductsPageQuery(int pageNo, int pageSize)
    {
        PageNo = pageNo;
        PageSize = pageSize;
    }
}