using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;

namespace ShopProject.Persistence.Configuration;

public class ProductConfiguration : IBaseConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProductName)
            .IsRequired();
        
        builder.Property(e => e.ProductPrice)
            .IsRequired();

        builder.HasMany(e => e.Categories)
            .WithMany(e => e.Products);

        builder.HasMany(e => e.Orders)
            .WithMany(e => e.Products);

        builder.HasMany(e => e.ProductImages)
            .WithOne(e => e.Prodcut)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}