using Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace Fleet.Application.Vehicles.Queries.ListTenantVehicles;

public class ListTenantVehiclesQuery : IRequest<List<VehicleDto>>
{
    public string TenantId { get; set; } = string.Empty;
}
