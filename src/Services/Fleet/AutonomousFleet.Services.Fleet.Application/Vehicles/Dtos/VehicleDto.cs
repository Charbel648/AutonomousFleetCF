using AutonomousFleet.Services.Fleet.Domain.Enums;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;

public class VehicleDto
{
    public string Id { get; set; } = string.Empty;

    public string TenantId { get; set; } = string.Empty;

    public string RegistrationNumber { get; set; } = string.Empty;

    public VehicleStatus Status { get; set; }

    public decimal BatteryLevel { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public DateTime LastTelemetryAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
