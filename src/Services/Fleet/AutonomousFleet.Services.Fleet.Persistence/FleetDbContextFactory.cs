using AutonomousFleet.Services.Fleet.Persistence.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutonomousFleet.Services.Fleet.Persistence;

public class FleetDbContextFactory : IDesignTimeDbContextFactory<FleetDbContext>
{
    public FleetDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FleetDbContext>()
            .UseNpgsql("Host=localhost;Port=5433;Database=fleetdb;Username=fleet;Password=fleet")
            .Options;

        return new FleetDbContext(options, new DesignTimeTenantContext());
    }
}
