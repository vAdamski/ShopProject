using ShopProject.Shared.Dtos;
using ShopProject.Shared.Interfaces;

namespace ShopProjectMauiBlazorApp.Services;

public class MauiCartService(IPreferencesStoreClone preferencesStoreClone) : ICartService
{
    public event Action? OnChange;
    private static string CART_NAME = "cart.txt";
    
    public async Task AddToCart(ProductMinimumInfoDto product)
    {
        var cart = preferencesStoreClone.Get<List<ProductMinimumInfoDto>>(CART_NAME);

        if (cart == null)
        {
            cart = new List<ProductMinimumInfoDto>();
        }

        cart.Add(product);

        preferencesStoreClone.Set(CART_NAME, cart);

        OnChange.Invoke();
    }

    public Task ClearCart()
    {
        preferencesStoreClone.Delete(CART_NAME);
        
        OnChange.Invoke();
        
        return Task.CompletedTask;
    }

    public async Task<List<ProductMinimumInfoDto>> GetProductsFromInCart()
    {
        var cart = preferencesStoreClone.Get<List<ProductMinimumInfoDto>>(CART_NAME);

        if (cart == null)
            return new();
        
        return cart;
    }

    public async Task RemoveFromCart(Guid productId)
    {
        var cart = preferencesStoreClone.Get<List<ProductMinimumInfoDto>>(CART_NAME);

        if (cart == null) return;

        var product = cart.FirstOrDefault(x => x.ProductId == productId);
        
        if (product == null) return;
        
        cart.Remove(product);
        
        preferencesStoreClone.Set(CART_NAME, cart);
        
        OnChange.Invoke();
    }
}