using Microsoft.EntityFrameworkCore;
using Order.Application.Abstractions.Persistence;
using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Persistance.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OrderEntity order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<OrderEntity>> GetByTenantAsync(string tenantId, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(order => order.TenantId == tenantId)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderEntity?> GetByIdAsync(Guid orderId, string tenantId, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(order =>
                order.OrderId == orderId &&
                order.TenantId == tenantId,
                cancellationToken);
    }
}
