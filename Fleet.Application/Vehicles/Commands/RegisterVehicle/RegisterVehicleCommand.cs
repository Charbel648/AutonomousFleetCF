using Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommand : IRequest<VehicleDto>
{
    public string RegistrationNumber { get; set; } = string.Empty;

    public int BatteryLevel { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string TelemetryData { get; set; } = "{}";
}
