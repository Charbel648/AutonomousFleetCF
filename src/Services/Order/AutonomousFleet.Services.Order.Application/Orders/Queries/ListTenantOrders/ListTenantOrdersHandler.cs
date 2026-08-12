using AutonomousFleet.Services.Order.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Order.Application.Orders.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Order.Application.Orders.Queries.ListTenantOrders;

public class ListTenantOrdersHandler : IRequestHandler<ListTenantOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public ListTenantOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderDto>> Handle(
        ListTenantOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByTenantAsync(cancellationToken);

        return orders.Select(order => order.ToDto()).ToList();
    }
}
