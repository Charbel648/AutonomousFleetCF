using AutonomousFleet.Services.Fleet.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.ModifyVehicleState;

public class ModifyVehicleStateHandler : IRequestHandler<ModifyVehicleStateCommand, VehicleDto?>
{
    private readonly IVehicleRepository _vehicleRepository;

    public ModifyVehicleStateHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<VehicleDto?> Handle(
        ModifyVehicleStateCommand request,
        CancellationToken cancellationToken)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);

        if (vehicle is null)
            return null;

        vehicle.ModifyState(request.Status);

        await _vehicleRepository.UpdateAsync(vehicle, cancellationToken);

        return vehicle.ToDto();
    }
}
