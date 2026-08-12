using AutonomousFleet.Services.Fleet.Domain.Enums;

namespace AutonomousFleet.Services.Fleet.Domain.Entities;

public class Vehicle
{
    private Vehicle()
    {
    }

    public Vehicle(
        string tenantId,
        string registrationNumber,
        decimal batteryLevel,
        decimal latitude,
        decimal longitude)
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new ArgumentException("Tenant id is required");

        if (string.IsNullOrWhiteSpace(registrationNumber))
            throw new ArgumentException("Registration number is required");

        ValidateBatteryLevel(batteryLevel);

        VehicleId = Guid.NewGuid().ToString();
        TenantId = tenantId;
        RegistrationNumber = registrationNumber;
        BatteryLevel = batteryLevel;
        Latitude = latitude;
        Longitude = longitude;
        Status = VehicleStatus.Available;
        LastTelemetryAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public string VehicleId { get; private set; } = string.Empty;

    public string Id => VehicleId;

    public string TenantId { get; private set; } = string.Empty;

    public string RegistrationNumber { get; private set; } = string.Empty;

    public VehicleStatus Status { get; private set; }

    public decimal BatteryLevel { get; private set; }

    public decimal Latitude { get; private set; }

    public decimal Longitude { get; private set; }

    public DateTime LastTelemetryAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastUpdatedAt { get; private set; }

    public void UpdateTelemetry(decimal batteryLevel, decimal latitude, decimal longitude)
    {
        ValidateBatteryLevel(batteryLevel);

        BatteryLevel = batteryLevel;
        Latitude = latitude;
        Longitude = longitude;
        LastTelemetryAt = DateTime.UtcNow;
        Touch();
    }

    public void ModifyState(VehicleStatus status)
    {
        if (Status == VehicleStatus.Offline && status == VehicleStatus.Running)
            throw new InvalidOperationException("Offline vehicles cannot directly enter running state");

        Status = status;
        Touch();
    }

    public void Assign()
    {
        if (Status != VehicleStatus.Available)
            throw new InvalidOperationException("Only available vehicles can be assigned");

        Status = VehicleStatus.Assigned;
        Touch();
    }

    public void MarkOffline()
    {
        Status = VehicleStatus.Offline;
        Touch();
    }

    private static void ValidateBatteryLevel(decimal batteryLevel)
    {
        if (batteryLevel < 0 || batteryLevel > 100)
            throw new ArgumentOutOfRangeException(nameof(batteryLevel), "Battery level must be between 0 and 100");
    }

    private void Touch()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}
