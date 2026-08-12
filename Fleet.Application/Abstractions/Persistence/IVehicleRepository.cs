using Fleet.Domain.Entities;

namespace Fleet.Application.Abstractions.Persistence;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

    Task<List<Vehicle>> GetByTenantAsync(CancellationToken cancellationToken);

    Task<Vehicle?> GetByIdAsync(Guid vehicleId, CancellationToken cancellationToken);

    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
}
