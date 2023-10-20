using ShopProject.Shared.Dtos;

namespace ShopProject.Shared.ViewModels;

public class ProductsViewModel
{
    public int PageSize { get; set; }
    public int CurrentPageNo { get; set; }
    public int MaxPageNo { get; set; }
    public List<ProductMinimumInfoDto> Products { get; set; } = new();
}