using Fleet.Application.Abstractions.Persistence;
using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Persistance.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly FleetDbContext _context;

    public VehicleRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        await _context.Vehicles.AddAsync(vehicle, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Vehicle>> GetByTenantAsync(string tenantId, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .AsNoTracking()
            .Where(vehicle => vehicle.TenantId == tenantId)
            .OrderBy(vehicle => vehicle.RegistrationNumber)
            .ToListAsync(cancellationToken);
    }

    public async Task<Vehicle?> GetByIdAsync(Guid vehicleId, string tenantId, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(vehicle =>
                vehicle.VehicleId == vehicleId &&
                vehicle.TenantId == tenantId,
                cancellationToken);
    }

    public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
