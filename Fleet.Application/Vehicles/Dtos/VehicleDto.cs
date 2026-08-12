namespace Fleet.Application.Vehicles.Dtos;

public class VehicleDto
{
    public Guid Id { get; set; }

    public string RegistrationNumber { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public int BatteryLevel { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string TelemetryData { get; set; } = "{}";

    public DateTime LastTelemetryAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
