namespace Fleet.Application.Abstractions.Tenancy;

public interface ITenantContextSetter
{
    void SetTenantId(string tenantId);
}
