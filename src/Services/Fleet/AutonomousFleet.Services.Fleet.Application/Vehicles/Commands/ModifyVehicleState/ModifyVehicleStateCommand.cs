using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using AutonomousFleet.Services.Fleet.Domain.Enums;
using MediatR;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.ModifyVehicleState;

public class ModifyVehicleStateCommand : IRequest<VehicleDto?>
{
    public string VehicleId { get; set; } = string.Empty;

    public VehicleStatus Status { get; set; }
}
