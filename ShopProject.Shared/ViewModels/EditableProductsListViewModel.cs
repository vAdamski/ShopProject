using ShopProject.Shared.Dtos.Products;

namespace ShopProject.Shared.ViewModels;

public class EditableProductsListViewModel
{
    public int PageSize { get; set; }
    public int CurrentPageNo { get; set; }
    public int MaxPageNo { get; set; }
    public List<EditableProductDto> Products { get; set; } = new();
}