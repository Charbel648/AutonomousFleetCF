using Fleet.Application.Abstractions.Tenancy;

namespace Fleet.Persistance.Tenancy;

public class DesignTimeTenantContext : ITenantContext
{
    public string TenantId => "design-time";

    public bool HasTenant => true;
}
