using AutonomousFleet.Services.Fleet.Domain.Enums;

namespace AutonomousFleet.Services.Fleet.API.Contracts;

public class ModifyVehicleStateRequest
{
    public VehicleStatus Status { get; set; }
}
