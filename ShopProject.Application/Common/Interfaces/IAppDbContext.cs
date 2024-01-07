using Microsoft.EntityFrameworkCore;
using ShopProject.Domain.Entities;

namespace ShopProject.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<ProductCategory> ProductCategories { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
    DbSet<Order> Orders { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(string createdBy, CancellationToken cancellationToken = default);
}