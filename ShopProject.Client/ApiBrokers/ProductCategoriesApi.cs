using ShopProject.Shared.Dtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Client.ApiBrokers;

public class ProductCategoriesApi : ApiBroker, IProductCategoriesApi
{
    public ProductCategoriesApi(HttpClient httpClient) : base(httpClient)
    {
    }
    
    private const string ProductCategoryRelativeUrl = "https://localhost:6001/api/product-categories";

    public async Task CreateProductCategory(CreateProductCategoryDto createProductCategoryDto) =>
         await PostAsJsonAsync(ProductCategoryRelativeUrl, createProductCategoryDto);

    public async Task<ProductCategoryListViewModel> GetListProductCategories() =>
        await GetFromJsonAsync<ProductCategoryListViewModel>($"{ProductCategoryRelativeUrl}/all");
}