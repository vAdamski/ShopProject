using ShopProject.Shared.Dtos;

namespace ShopProject.Client.ApiBrokers;

public class ProductApi : ApiBroker, IProductApi
{
    public ProductApi(HttpClient httpClient) : base(httpClient)
    {
    }
    
    private const string ProductRelativeUrl = "https://localhost:6001/api/products";

    public async Task CreateProduct(MultipartFormDataContent multipartFormDataContent)
    {
        await PostAsync<MultipartFormDataContent>($"{ProductRelativeUrl}/create-product", multipartFormDataContent);
    }
    
}

public interface IProductApi
{
    Task CreateProduct(MultipartFormDataContent multipartFormDataContent);
}