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
        var vehicle = await _mediator.Send(command, cancellationToken);

        return Created($"/vehicles/{vehicle.Id}", vehicle);
    }

    [HttpGet]
    public async Task<ActionResult<List<VehicleDto>>> ListTenantVehicles(CancellationToken cancellationToken)
    {
        var vehicles = await _mediator.Send(new ListTenantVehiclesQuery(), cancellationToken);

        return Ok(vehicles);
    }

    [HttpPatch("{id:guid}/state")]
    public async Task<ActionResult<VehicleDto>> ModifyVehicleState(
        [FromRoute] Guid id,
        [FromBody] ModifyVehicleStateCommand command,
        CancellationToken cancellationToken)
    {
        command.VehicleId = id;

        var vehicle = await _mediator.Send(command, cancellationToken);

        if (vehicle is null)
            return NotFound(new { Message = $"Vehicle '{id}' was not found" });

        return Ok(vehicle);
    }
}
