using AutonomousFleet.Services.Order.Application.Orders.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public string CustomerId { get; set; } = string.Empty;

    public string PickupAddress { get; set; } = string.Empty;

    public string DeliveryAddress { get; set; } = string.Empty;
}
