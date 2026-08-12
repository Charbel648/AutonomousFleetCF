using Fleet.Domain.Entities;

namespace Fleet.Application.Vehicles.Dtos;

public static class VehicleMappings
{
    public static VehicleDto ToDto(this Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            RegistrationNumber = vehicle.RegistrationNumber,
            Status = vehicle.Status.ToString(),
            BatteryLevel = vehicle.BatteryLevel,
            Latitude = vehicle.Latitude,
            Longitude = vehicle.Longitude,
            TelemetryData = vehicle.TelemetryData,
            LastTelemetryAt = vehicle.LastTelemetryAt,
            CreatedAt = vehicle.CreatedAt,
            LastUpdatedAt = vehicle.LastUpdatedAt
        };
    }
}
