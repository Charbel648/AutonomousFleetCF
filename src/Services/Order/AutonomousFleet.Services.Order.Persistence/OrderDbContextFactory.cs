using AutonomousFleet.Services.Order.Persistence.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutonomousFleet.Services.Order.Persistence;

public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseNpgsql("Host=localhost;Port=5434;Database=orderdb;Username=orders;Password=orders")
            .Options;

        return new OrderDbContext(options, new DesignTimeTenantContext());
    }
}
