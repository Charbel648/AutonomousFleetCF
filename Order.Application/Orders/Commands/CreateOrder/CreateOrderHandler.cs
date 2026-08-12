using MediatR;
using Order.Application.Abstractions.Persistence;
using Order.Application.Abstractions.Tenancy;
using Order.Application.Orders.Dtos;
using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITenantContext _tenantContext;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        ITenantContext tenantContext)
    {
        _orderRepository = orderRepository;
        _tenantContext = tenantContext;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!_tenantContext.HasTenant)
            throw new InvalidOperationException("Tenant context is required");

        var order = new OrderEntity(
            _tenantContext.TenantId,
            request.CustomerId,
            request.PickupAddress,
            request.DeliveryAddress);

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.ToDto();
    }
}
