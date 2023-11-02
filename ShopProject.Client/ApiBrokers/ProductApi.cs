using ShopProject.Shared.Dtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Client.ApiBrokers;

public class ProductApi : ApiBroker, IProductApi
{
    public ProductApi(HttpClient httpClient) : base(httpClient)
    {
    }

    private const string ProductRelativeUrl = "https://localhost:6001/api/products";

    public async Task CreateProduct(MultipartFormDataContent multipartFormDataContent) =>
        await PostAsync<MultipartFormDataContent>($"{ProductRelativeUrl}/create-product", multipartFormDataContent);
    
    public async Task<EditableProductsListViewModel> GetEditableProducts() =>
        await GetFromJsonAsync<EditableProductsListViewModel>($"{ProductRelativeUrl}/get-editable-products");
}

public interface IProductApi
{
    Task CreateProduct(MultipartFormDataContent multipartFormDataContent);
    Task<EditableProductsListViewModel> GetEditableProducts();
}