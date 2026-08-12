using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Queries.ListTenantVehicles;

public class ListTenantVehiclesQuery : IRequest<List<VehicleDto>>
{
}
