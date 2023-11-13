using ShopProject.Shared.Dtos;

namespace ShopProject.Client.Services;

public interface ICartService
{
    event Action OnChange;
    Task AddToCart(ProductMinimumInfoDto product);
    Task<List<ProductMinimumInfoDto>> GetProductsFromInCart();
    Task RemoveFromCart(Guid productId);
}