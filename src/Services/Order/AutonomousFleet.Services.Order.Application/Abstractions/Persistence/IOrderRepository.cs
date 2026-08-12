using AutonomousFleet.Services.Order.Domain.Entities;

namespace AutonomousFleet.Services.Order.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);

    Task<List<Order>> GetByTenantAsync(CancellationToken cancellationToken);

    Task<Order?> GetByIdAsync(string orderId, CancellationToken cancellationToken);
}
