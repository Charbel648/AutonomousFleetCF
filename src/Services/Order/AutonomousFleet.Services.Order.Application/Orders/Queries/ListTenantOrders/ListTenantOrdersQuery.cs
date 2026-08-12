using AutonomousFleet.Services.Order.Application.Orders.Dtos;
using MediatR;

namespace AutonomousFleet.Services.Order.Application.Orders.Queries.ListTenantOrders;

public class ListTenantOrdersQuery : IRequest<List<OrderDto>>
{
}
