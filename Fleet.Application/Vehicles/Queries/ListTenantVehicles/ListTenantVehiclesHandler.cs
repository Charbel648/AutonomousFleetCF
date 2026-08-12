using Fleet.Application.Abstractions.Persistence;
using Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace Fleet.Application.Vehicles.Queries.ListTenantVehicles;

public class ListTenantVehiclesHandler : IRequestHandler<ListTenantVehiclesQuery, List<VehicleDto>>
{
    private readonly IVehicleRepository _vehicleRepository;

    public ListTenantVehiclesHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<VehicleDto>> Handle(ListTenantVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await _vehicleRepository.GetByTenantAsync(request.TenantId, cancellationToken);

        return vehicles.Select(vehicle => vehicle.ToDto()).ToList();
    }
}
