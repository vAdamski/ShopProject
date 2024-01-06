using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Common;
using ShopProject.Domain.Entities;

namespace ShopProject.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _userService;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IDateTime dateTime,
        ICurrentUserService userService) : base(options)
    {
        _dateTime = dateTime;
        _userService = userService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.Entity.ModifiedBy = _userService.Email;
                    entry.Entity.Modified = _dateTime.Now;
                    entry.Entity.Inactivated = _dateTime.Now;
                    entry.Entity.InactivatedBy = _userService.Email;
                    entry.Entity.StatusId = 0;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _userService.Email;
                    entry.Entity.Modified = _dateTime.Now;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedBy = _userService.Email;
                    entry.Entity.Created = _dateTime.Now;
                    entry.Entity.StatusId = 1;
                    break;
                default:
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ValueObject>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    break;
                default:
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}