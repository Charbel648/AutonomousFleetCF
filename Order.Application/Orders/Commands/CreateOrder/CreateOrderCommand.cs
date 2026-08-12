using System.Text.Json.Serialization;
using MediatR;
using Order.Application.Orders.Dtos;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderDto>
{
    [JsonIgnore]
    public string TenantId { get; set; } = string.Empty;

    public string CustomerId { get; set; } = string.Empty;

    public string PickupAddress { get; set; } = string.Empty;

    public string DeliveryAddress { get; set; } = string.Empty;
}
