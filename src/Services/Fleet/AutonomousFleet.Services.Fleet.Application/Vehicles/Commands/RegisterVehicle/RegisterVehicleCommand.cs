using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommand : IRequest<VehicleDto>
{
    public string RegistrationNumber { get; set; } = string.Empty;

    public decimal BatteryLevel { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
