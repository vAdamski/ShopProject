using ShopProject.Shared.Dtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Client.ApiBrokers;

public interface IProductCategoriesApi
{
    Task CreateProductCategory(CreateProductCategoryDto createProductCategoryDto);
    Task<ProductCategoryListViewModel> GetListProductCategories();
}