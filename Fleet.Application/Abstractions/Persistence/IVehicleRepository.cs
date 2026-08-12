using Fleet.Domain.Entities;

namespace Fleet.Application.Abstractions.Persistence;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

    Task<List<Vehicle>> GetByTenantAsync(string tenantId, CancellationToken cancellationToken);

    Task<Vehicle?> GetByIdAsync(Guid vehicleId, string tenantId, CancellationToken cancellationToken);

    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
}
