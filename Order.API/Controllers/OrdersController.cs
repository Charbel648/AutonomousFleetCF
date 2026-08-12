using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.API.Auth;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Application.Orders.Dtos;
using Order.Application.Orders.Queries.GetOrderDetails;
using Order.Application.Orders.Queries.ListTenantOrders;

namespace Order.API.Controllers;

[ApiController]
[Route("orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = PermissionNames.CanCreateOrder)]
    [RequirePermission(PermissionNames.CanCreateOrder)]
    public async Task<ActionResult<OrderDto>> CreateOrder(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetOrderDetails), new { id = order.Id }, order);
    }

    [HttpGet]
    [Authorize(Policy = PermissionNames.CanReadOrders)]
    [RequirePermission(PermissionNames.CanReadOrders)]
    public async Task<ActionResult<List<OrderDto>>> ListTenantOrders(CancellationToken cancellationToken)
    {
        var orders = await _mediator.Send(new ListTenantOrdersQuery(), cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = PermissionNames.CanReadOrders)]
    [RequirePermission(PermissionNames.CanReadOrders)]
    public async Task<ActionResult<OrderDto>> GetOrderDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderDetailsQuery
        {
            OrderId = id
        }, cancellationToken);

        if (order is null)
            return NotFound(new { Message = $"Order '{id}' was not found" });

        return Ok(order);
    }
}
