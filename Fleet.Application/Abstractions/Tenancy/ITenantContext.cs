namespace Fleet.Application.Abstractions.Tenancy;

public interface ITenantContext
{
    string TenantId { get; }

    bool HasTenant { get; }
}
