using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Application.Orders.Dtos;
using Order.Application.Orders.Queries.GetOrderDetails;
using Order.Application.Orders.Queries.ListTenantOrders;

namespace Order.API.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private const string TenantHeaderName = "X-Tenant-Id";
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        if (!TryGetTenantId(out var tenantId))
            return BadRequest(new { Message = "Missing X-Tenant-Id header" });

        command.TenantId = tenantId;

        var order = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetOrderDetails), new { id = order.Id }, order);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> ListTenantOrders(CancellationToken cancellationToken)
    {
        if (!TryGetTenantId(out var tenantId))
            return BadRequest(new { Message = "Missing X-Tenant-Id header" });

        var orders = await _mediator.Send(new ListTenantOrdersQuery
        {
            TenantId = tenantId
        }, cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetOrderDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        if (!TryGetTenantId(out var tenantId))
            return BadRequest(new { Message = "Missing X-Tenant-Id header" });

        var order = await _mediator.Send(new GetOrderDetailsQuery
        {
            OrderId = id,
            TenantId = tenantId
        }, cancellationToken);

        if (order is null)
            return NotFound(new { Message = $"Order '{id}' was not found" });

        return Ok(order);
    }

    private bool TryGetTenantId(out string tenantId)
    {
        tenantId = string.Empty;

        if (!Request.Headers.TryGetValue(TenantHeaderName, out var value))
            return false;

        tenantId = value.ToString().Trim();

        return !string.IsNullOrWhiteSpace(tenantId);
    }
}
