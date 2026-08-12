using AutonomousFleet.Services.Fleet.Domain.Entities;

namespace AutonomousFleet.Services.Fleet.Application.Abstractions.Persistence;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

    Task<List<Vehicle>> GetByTenantAsync(CancellationToken cancellationToken);

    Task<Vehicle?> GetByIdAsync(string vehicleId, CancellationToken cancellationToken);

    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
}
