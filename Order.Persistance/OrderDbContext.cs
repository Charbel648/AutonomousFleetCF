using Microsoft.EntityFrameworkCore;
using Order.Application.Abstractions.Tenancy;
using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Persistance;

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

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);

        modelBuilder.Entity<OrderEntity>()
            .HasQueryFilter(order => order.TenantId == _tenantContext.TenantId);

        base.OnModelCreating(modelBuilder);
    }
}
