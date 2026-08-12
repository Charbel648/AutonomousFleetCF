using AutonomousFleet.Services.Fleet.Domain.Entities;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;

public static class VehicleMappings
{
    public static VehicleDto ToDto(this Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            TenantId = vehicle.TenantId,
            RegistrationNumber = vehicle.RegistrationNumber,
            Status = vehicle.Status,
            BatteryLevel = vehicle.BatteryLevel,
            Latitude = vehicle.Latitude,
            Longitude = vehicle.Longitude,
            LastTelemetryAt = vehicle.LastTelemetryAt,
            CreatedAt = vehicle.CreatedAt,
            LastUpdatedAt = vehicle.LastUpdatedAt
        };
    }
}
