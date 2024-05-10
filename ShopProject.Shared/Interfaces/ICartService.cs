using ShopProject.Shared.Dtos;

namespace ShopProject.Shared.Interfaces;

public interface ICartService
{
    event Action? OnChange;
    Task AddToCart(ProductMinimumInfoDto product);
    Task ClearCart();
    Task<List<ProductMinimumInfoDto>> GetProductsFromInCart();
    Task RemoveFromCart(Guid productId);
}