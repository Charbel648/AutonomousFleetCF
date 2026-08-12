using AutonomousFleet.Services.Fleet.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Queries.ListTenantVehicles;

public class ListTenantVehiclesHandler : IRequestHandler<ListTenantVehiclesQuery, List<VehicleDto>>
{
    private readonly IVehicleRepository _vehicleRepository;

    public ListTenantVehiclesHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<VehicleDto>> Handle(
        ListTenantVehiclesQuery request,
        CancellationToken cancellationToken)
    {
        var vehicles = await _vehicleRepository.GetByTenantAsync(cancellationToken);

        return vehicles.Select(vehicle => vehicle.ToDto()).ToList();
    }
}
