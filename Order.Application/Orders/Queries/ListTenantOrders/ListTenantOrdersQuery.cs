using MediatR;
using Order.Application.Orders.Dtos;

namespace Order.Application.Orders.Queries.ListTenantOrders;

public class ListTenantOrdersQuery : IRequest<List<OrderDto>>
{
    public string TenantId { get; set; } = string.Empty;
}
