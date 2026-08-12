using Fleet.Persistance.Tenancy;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Persistance;

public class FleetDbContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<FleetDbContext>
{
    public FleetDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FleetDbContext>()
            .UseNpgsql("Host=localhost;Port=5433;Database=fleetdb;Username=fleet;Password=fleet")
            .Options;

        return new FleetDbContext(options, new DesignTimeTenantContext());
    }
}
