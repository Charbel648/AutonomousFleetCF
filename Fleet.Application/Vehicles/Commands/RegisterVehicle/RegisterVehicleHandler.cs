using Fleet.Application.Abstractions.Persistence;
using Fleet.Application.Vehicles.Dtos;
using Fleet.Domain.Entities;
using MediatR;

namespace Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleHandler : IRequestHandler<RegisterVehicleCommand, VehicleDto>
{
    private readonly IVehicleRepository _vehicleRepository;

    public RegisterVehicleHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<VehicleDto> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle(
            request.TenantId,
            request.RegistrationNumber,
            request.BatteryLevel,
            request.Latitude,
            request.Longitude);

        await _vehicleRepository.AddAsync(vehicle, cancellationToken);

        return vehicle.ToDto();
    }
}
