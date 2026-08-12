using MediatR;
using Order.Application.Abstractions.Persistence;
using Order.Application.Orders.Dtos;
using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new OrderEntity(
            request.TenantId,
            request.CustomerId,
            request.PickupAddress,
            request.DeliveryAddress);

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.ToDto();
    }
}
