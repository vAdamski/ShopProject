using Blazored.LocalStorage;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.Interfaces;

namespace ShopProject.Client.Services;


public class CartService : ICartService
{
    private static string CART_NAME = "cart";
    private readonly ILocalStorageService _localStorageService;
    public event Action? OnChange;

    public CartService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    public async Task AddToCart(ProductMinimumInfoDto product)
    {
        var cart = await _localStorageService.GetItemAsync<List<ProductMinimumInfoDto>>(CART_NAME);

        if (cart == null)
        {
            cart = new List<ProductMinimumInfoDto>();
        }

        cart.Add(product);

        await _localStorageService.SetItemAsync(CART_NAME, cart);

        OnChange.Invoke();
    }
    
    public async Task ClearCart()
    {
        await _localStorageService.RemoveItemAsync(CART_NAME);
        OnChange.Invoke();
    }
    

    public async Task<List<ProductMinimumInfoDto>> GetProductsFromInCart()
    {
        return await _localStorageService.GetItemAsync<List<ProductMinimumInfoDto>>(CART_NAME);
    }

    public async Task RemoveFromCart(Guid productId)
    {
        var cart = await _localStorageService.GetItemAsync<List<ProductMinimumInfoDto>>(CART_NAME);

        if (cart == null) return;

        var product = cart.FirstOrDefault(x => x.ProductId == productId);
        
        if (product == null) return;
        
        cart.Remove(product);
        
        await _localStorageService.SetItemAsync(CART_NAME, cart);
        
        OnChange.Invoke();
    }
}