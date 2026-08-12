using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task AddAsync(OrderEntity order, CancellationToken cancellationToken);

    Task<List<OrderEntity>> GetByTenantAsync(string tenantId, CancellationToken cancellationToken);

    Task<OrderEntity?> GetByIdAsync(Guid orderId, string tenantId, CancellationToken cancellationToken);
}
