using OrderEntity = AutonomousFleet.Services.Order.Domain.Entities.Order;

namespace AutonomousFleet.Services.Order.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task AddAsync(OrderEntity order, CancellationToken cancellationToken);

    Task<List<OrderEntity>> GetByTenantAsync(CancellationToken cancellationToken);

    Task<OrderEntity?> GetByIdAsync(string orderId, CancellationToken cancellationToken);
}
