using AutonomousFleet.Services.Order.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutonomousFleet.Services.Order.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Order>> GetByTenantAsync(CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(string orderId, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.OrderId == orderId, cancellationToken);
    }
}
