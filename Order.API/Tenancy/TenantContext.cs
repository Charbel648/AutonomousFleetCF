using Order.Application.Abstractions.Tenancy;

namespace Order.API.Tenancy;

public class TenantContext : ITenantContext, ITenantContextSetter
{
    public string TenantId { get; private set; } = string.Empty;

    public bool HasTenant => !string.IsNullOrWhiteSpace(TenantId);

    public void SetTenantId(string tenantId)
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new ArgumentException("Tenant id cannot be empty", nameof(tenantId));

        TenantId = tenantId.Trim();
    }
}
