namespace Order.Application.Abstractions.Tenancy;

public interface ITenantContextSetter
{
    void SetTenantId(string tenantId);
}
