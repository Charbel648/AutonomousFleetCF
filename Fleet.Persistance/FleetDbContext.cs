using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Persistance;

public class FleetDbContext : DbContext
{
    public FleetDbContext(DbContextOptions<FleetDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
