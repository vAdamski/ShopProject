using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopProject.Application.Common.Interfaces;

public interface IBaseConfiguration<T> where T : class
{
    public void Configure(EntityTypeBuilder<T> builder);
}