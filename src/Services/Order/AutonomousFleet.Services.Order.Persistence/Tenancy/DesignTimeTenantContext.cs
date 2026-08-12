using AutonomousFleet.Services.Order.Application.Abstractions.Tenancy;

namespace AutonomousFleet.Services.Order.Persistence.Tenancy;

public class DesignTimeTenantContext : ITenantContext
{
    public string TenantId => "design-time";

    public bool HasTenant => true;
}
