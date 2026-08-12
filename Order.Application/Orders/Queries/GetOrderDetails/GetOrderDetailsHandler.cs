using MediatR;
using Order.Application.Abstractions.Persistence;
using Order.Application.Orders.Dtos;

namespace Order.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailsHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            request.TenantId,
            cancellationToken);

        return order?.ToDto();
    }
}
