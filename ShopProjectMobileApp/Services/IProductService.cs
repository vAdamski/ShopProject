using ShopProject.Shared.ViewModels;

namespace ShopProjectMobileApp.Services;

public interface IProductService
{
    Task<ProductsViewModel> GetProductsViewModel();
}