using AutonomousFleet.Services.Fleet.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Fleet.Application.Abstractions.Tenancy;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using AutonomousFleet.Services.Fleet.Domain.Entities;
using MediatR;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleHandler : IRequestHandler<RegisterVehicleCommand, VehicleDto>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ITenantContext _tenantContext;

    public RegisterVehicleHandler(
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
        if (!_tenantContext.HasTenant)
            throw new InvalidOperationException("Tenant context is required");

        var vehicle = new Vehicle(
            _tenantContext.TenantId,
            request.RegistrationNumber,
            request.BatteryLevel,
            request.Latitude,
            request.Longitude);

        await _vehicleRepository.AddAsync(vehicle, cancellationToken);

        return vehicle.ToDto();
    }
}
