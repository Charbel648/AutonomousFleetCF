using AutonomousFleet.Services.Order.Application.Abstractions.Tenancy;
using AutonomousFleet.Services.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutonomousFleet.Services.Order.Persistence;

public class OrderDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    public OrderDbContext(
        DbContextOptions<OrderDbContext> options,
        ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);

        modelBuilder.Entity<Order>()
            .HasQueryFilter(order => order.TenantId == _tenantContext.TenantId);

        base.OnModelCreating(modelBuilder);
    }
}
