using ShopProject.Shared.Dtos;

namespace ShopProject.Shared.ViewModels;

public class ProductCategoryListViewModel
{
    public List<ProductCategoryDto> ProductCategories { get; set; } = new();
}