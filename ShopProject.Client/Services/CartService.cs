using Blazored.LocalStorage;
using ShopProject.Shared.Dtos;

namespace ShopProject.Client.Services;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorageService;
    public event Action? OnChange;

    public CartService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task AddToCart(ProductMinimumInfoDto product)
    {
        var cart = await _localStorageService.GetItemAsync<List<ProductMinimumInfoDto>>("cart");

        if (cart == null)
        {
            cart = new List<ProductMinimumInfoDto>();
        }

        cart.Add(product);

        await _localStorageService.SetItemAsync("cart", cart);

        OnChange.Invoke();
    }

    public async Task<List<ProductMinimumInfoDto>> GetProductsFromInCart()
    {
        return await _localStorageService.GetItemAsync<List<ProductMinimumInfoDto>>("cart");
    }

    public async Task RemoveFromCart(Guid productId)
    {
        var cart = await _localStorageService.GetItemAsync<List<ProductMinimumInfoDto>>("cart");

        if (cart == null) return;

        var product = cart.FirstOrDefault(x => x.ProductId == productId);
        
        if (product == null) return;
        
        cart.Remove(product);
        
        await _localStorageService.SetItemAsync("cart", cart);
        
        OnChange.Invoke();
    }
}