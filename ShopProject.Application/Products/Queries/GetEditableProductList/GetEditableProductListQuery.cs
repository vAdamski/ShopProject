using MediatR;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Products.Queries.GetEditableProductList;

public class GetEditableProductListQuery : IRequest<EditableProductsListViewModel>
{
    public int PageNo { get; }
    public int PageSize { get; }

    public GetEditableProductListQuery()
    {
        PageNo = 1;
        PageSize = 10;
    }

    public GetEditableProductListQuery(int pageNo, int pageSize)
    {
        PageNo = pageNo;
        PageSize = pageSize;
    }
}