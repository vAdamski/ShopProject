using Microsoft.EntityFrameworkCore;

namespace ShopProject.Persistence;

public class AppDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
    protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
    {
        return new AppDbContext(options);
    }
}