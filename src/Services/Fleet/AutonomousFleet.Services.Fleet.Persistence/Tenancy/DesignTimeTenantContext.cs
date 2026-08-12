using AutonomousFleet.Services.Fleet.Application.Abstractions.Tenancy;

namespace AutonomousFleet.Services.Fleet.Persistence.Tenancy;

public class DesignTimeTenantContext : ITenantContext
{
    public string TenantId => "design-time";

    public bool HasTenant => true;
}
