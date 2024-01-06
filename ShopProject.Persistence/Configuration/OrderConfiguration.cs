using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;

namespace ShopProject.Persistence.Configuration;

public class OrderConfiguration : IBaseConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.OrderState)
            .IsRequired();
        
        builder.Property(e => e.UserEmail)
            .IsRequired();

        builder.HasMany(e => e.Products)
            .WithMany(e => e.Orders);
    }
}