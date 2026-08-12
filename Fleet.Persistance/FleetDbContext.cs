using Fleet.Application.Abstractions.Tenancy;
using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Persistance;

public class FleetDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    public FleetDbContext(
        DbContextOptions<FleetDbContext> options,
        ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetDbContext).Assembly);

        modelBuilder.Entity<Vehicle>()
            .HasQueryFilter(vehicle => vehicle.TenantId == _tenantContext.TenantId);

        base.OnModelCreating(modelBuilder);
    }
}
