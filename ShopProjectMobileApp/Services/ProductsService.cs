using System.Diagnostics;
using System.Text.Json;
using ShopProject.Shared.ViewModels;
using ShopProjectMobileApp.Providers;

namespace ShopProjectMobileApp.Services;

public class ProductsService : BaseService, IProductService
{
    public async Task<ProductsViewModel> GetProductsViewModel()
    {
        using var _httpClient = HttpClientProvider.GetHttpClient();

        Uri uri = new Uri($"{_baseUrl}/api/products/get-products");

        ProductsViewModel productsViewModel;
        
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return productsViewModel = JsonSerializer.Deserialize<ProductsViewModel>(content, _jsonSerializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new ProductsViewModel
        {
            PageSize = 1,
            CurrentPageNo = 1,
            MaxPageNo = 1,
            Products = new ()
        };
    }
}