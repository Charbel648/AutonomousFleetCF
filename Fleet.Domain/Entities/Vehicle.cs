using Fleet.Domain.Enums;

namespace Fleet.Domain.Entities;

public class Vehicle
{
    private const int MinimumBatteryLevel = 0;
    private const int MaximumBatteryLevel = 100;

    public Guid VehicleId { get; private set; }

    public Guid Id => VehicleId;

    public string TenantId { get; private set; } = string.Empty;

    public string RegistrationNumber { get; private set; } = string.Empty;

    public VehicleStatus Status { get; private set; }

    public int BatteryLevel { get; private set; }

    public decimal Latitude { get; private set; }

    public decimal Longitude { get; private set; }

    public string TelemetryData { get; private set; } = "{}";

    public DateTime LastTelemetryAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastUpdatedAt { get; private set; }

    private Vehicle()
    {
    }

    public Vehicle(
        string tenantId,
        string registrationNumber,
        int batteryLevel,
        decimal latitude,
        decimal longitude,
        string telemetryData = "{}")
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new ArgumentException("Tenant id is required", nameof(tenantId));

        if (string.IsNullOrWhiteSpace(registrationNumber))
            throw new ArgumentException("Registration number is required", nameof(registrationNumber));

        ValidateBatteryLevel(batteryLevel);
        ValidateCoordinates(latitude, longitude);

        VehicleId = Guid.NewGuid();
        TenantId = tenantId.Trim();
        RegistrationNumber = registrationNumber.Trim();
        Status = VehicleStatus.Available;
        BatteryLevel = batteryLevel;
        Latitude = latitude;
        Longitude = longitude;
        TelemetryData = NormalizeTelemetryData(telemetryData);
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = CreatedAt;
        LastTelemetryAt = CreatedAt;
    }

    public void UpdateTelemetry(
        int batteryLevel,
        decimal latitude,
        decimal longitude,
        string telemetryData = "{}")
    {
        ValidateBatteryLevel(batteryLevel);
        ValidateCoordinates(latitude, longitude);

        BatteryLevel = batteryLevel;
        Latitude = latitude;
        Longitude = longitude;
        TelemetryData = NormalizeTelemetryData(telemetryData);
        LastTelemetryAt = DateTime.UtcNow;

        Touch();
    }

    public void ModifyState(VehicleStatus status)
    {
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

    private static void ValidateBatteryLevel(int batteryLevel)
    {
        if (batteryLevel < MinimumBatteryLevel || batteryLevel > MaximumBatteryLevel)
            throw new ArgumentOutOfRangeException(
                nameof(batteryLevel),
                "Battery level must be between 0 and 100");
    }

    private static void ValidateCoordinates(decimal latitude, decimal longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90");

        if (longitude < -180 || longitude > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180");
    }

    private static string NormalizeTelemetryData(string telemetryData)
    {
        return string.IsNullOrWhiteSpace(telemetryData)
            ? "{}"
            : telemetryData.Trim();
    }

    private void Touch()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}
