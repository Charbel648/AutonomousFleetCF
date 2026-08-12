using Microsoft.EntityFrameworkCore;
using Order.Persistance.Tenancy;

namespace Order.Persistance;

public class OrderDbContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseNpgsql("Host=localhost;Port=5434;Database=orderdb;Username=orders;Password=orders")
            .Options;

        return new OrderDbContext(options, new DesignTimeTenantContext());
    }
}
