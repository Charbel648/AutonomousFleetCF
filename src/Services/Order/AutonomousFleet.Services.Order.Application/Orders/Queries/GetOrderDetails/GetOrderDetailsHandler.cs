using AutonomousFleet.Services.Order.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Order.Application.Orders.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Order.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailsHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(
        GetOrderDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        return order?.ToDto();
    }
}
