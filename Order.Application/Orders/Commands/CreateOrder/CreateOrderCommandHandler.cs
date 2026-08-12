using MediatR;
using Order.Application.Abstractions.Persistence;
using Order.Application.Abstractions.Tenancy;
using Order.Application.Orders.Dtos;
using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITenantContext _tenantContext;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ITenantContext tenantContext)
    {
        _orderRepository = orderRepository;
        _tenantContext = tenantContext;
    }

    public async Task<OrderDto> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = new OrderEntity(
            _tenantContext.TenantId,
            request.CustomerId,
            request.PickupAddress,
            request.DeliveryAddress,
            request.PriorityScore);

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.ToDto();
    }
}
