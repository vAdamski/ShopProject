using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;

namespace ShopProject.Persistence.Configuration;

public class ProductCategoryConfiguration : IBaseConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CategoryName)
            .IsRequired();

        builder.HasMany(e => e.Products)
            .WithMany(e => e.Categories);
    }
}