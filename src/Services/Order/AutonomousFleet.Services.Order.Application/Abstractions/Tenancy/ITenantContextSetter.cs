namespace AutonomousFleet.Services.Order.Application.Abstractions.Tenancy;

public interface ITenantContextSetter
{
    void SetTenantId(string tenantId);
}
