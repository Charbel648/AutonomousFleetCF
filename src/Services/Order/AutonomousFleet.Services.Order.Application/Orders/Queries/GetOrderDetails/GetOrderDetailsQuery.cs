using AutonomousFleet.Services.Order.Application.Orders.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Order.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQuery : IRequest<OrderDto?>
{
    public string OrderId { get; set; } = string.Empty;
}
