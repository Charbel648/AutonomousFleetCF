using MediatR;
using Order.Application.Orders.Dtos;

namespace Order.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQuery : IRequest<OrderDto?>
{
    public Guid OrderId { get; set; }
}
