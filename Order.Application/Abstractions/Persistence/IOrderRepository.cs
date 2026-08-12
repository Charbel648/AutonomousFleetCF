using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task AddAsync(OrderEntity order, CancellationToken cancellationToken);

    Task<List<OrderEntity>> GetByTenantAsync(CancellationToken cancellationToken);

    Task<OrderEntity?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken);
}
