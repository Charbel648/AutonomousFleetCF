using Fleet.Application.Abstractions.Persistence;
using Fleet.Application.Abstractions.Tenancy;
using Fleet.Application.Vehicles.Dtos;
using Fleet.Domain.Entities;
using MediatR;

namespace Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommandHandler : IRequestHandler<RegisterVehicleCommand, VehicleDto>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ITenantContext _tenantContext;

    public RegisterVehicleCommandHandler(
        IVehicleRepository vehicleRepository,
        ITenantContext tenantContext)
    {
        _vehicleRepository = vehicleRepository;
        _tenantContext = tenantContext;
    }

    public async Task<VehicleDto> Handle(
        RegisterVehicleCommand request,
        CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle(
            _tenantContext.TenantId,
            request.RegistrationNumber,
            request.BatteryLevel,
            request.Latitude,
            request.Longitude,
            request.TelemetryData);

        await _vehicleRepository.AddAsync(vehicle, cancellationToken);

        return vehicle.ToDto();
    }
}
