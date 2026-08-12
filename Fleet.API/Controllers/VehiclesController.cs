using Fleet.Application.Vehicles.Commands.ModifyVehicleState;
using Fleet.Application.Vehicles.Commands.RegisterVehicle;
using Fleet.Application.Vehicles.Dtos;
using Fleet.Application.Vehicles.Queries.ListTenantVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers;

[ApiController]
[Route("vehicles")]
public class VehiclesController : ControllerBase
{
    private const string TenantHeaderName = "X-Tenant-Id";
    private readonly IMediator _mediator;

    public VehiclesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> RegisterVehicle(
        [FromBody] RegisterVehicleCommand command,
        CancellationToken cancellationToken)
    {
        if (!TryGetTenantId(out var tenantId))
            return BadRequest(new { Message = "Missing X-Tenant-Id header" });

        command.TenantId = tenantId;

        var vehicle = await _mediator.Send(command, cancellationToken);

        return Created($"/vehicles/{vehicle.Id}", vehicle);
    }

    [HttpGet]
    public async Task<ActionResult<List<VehicleDto>>> ListTenantVehicles(CancellationToken cancellationToken)
    {
        if (!TryGetTenantId(out var tenantId))
            return BadRequest(new { Message = "Missing X-Tenant-Id header" });

        var vehicles = await _mediator.Send(new ListTenantVehiclesQuery
        {
            TenantId = tenantId
        }, cancellationToken);

        return Ok(vehicles);
    }

    [HttpPatch("{id:guid}/state")]
    public async Task<ActionResult<VehicleDto>> ModifyVehicleState(
        [FromRoute] Guid id,
        [FromBody] ModifyVehicleStateCommand command,
        CancellationToken cancellationToken)
    {
        if (!TryGetTenantId(out var tenantId))
            return BadRequest(new { Message = "Missing X-Tenant-Id header" });

        command.VehicleId = id;
        command.TenantId = tenantId;

        var vehicle = await _mediator.Send(command, cancellationToken);

        if (vehicle is null)
            return NotFound(new { Message = $"Vehicle '{id}' was not found" });

        return Ok(vehicle);
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
