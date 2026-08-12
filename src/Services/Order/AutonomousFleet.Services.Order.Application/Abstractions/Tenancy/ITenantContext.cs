namespace AutonomousFleet.Services.Order.Application.Abstractions.Tenancy;

public interface ITenantContext
{
    string TenantId { get; }

    bool HasTenant { get; }
}
