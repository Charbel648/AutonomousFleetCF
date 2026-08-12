using Order.Application.Abstractions.Tenancy;

namespace Order.Persistance.Tenancy;

public class DesignTimeTenantContext : ITenantContext
{
    public string TenantId => "design-time";

    public bool HasTenant => true;
}
