using AutonomousFleet.Services.Fleet.API.Contracts;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.ModifyVehicleState;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.RegisterVehicle;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Dtos;
using AutonomousFleet.Services.Fleet.Application.Vehicles.Queries.ListTenantVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutonomousFleet.Services.Fleet.API.Controllers;

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
    public async Task<ActionResult<List<VehicleDto>>> ListTenantVehicles(
        CancellationToken cancellationToken)
    {
        var vehicles = await _mediator.Send(new ListTenantVehiclesQuery(), cancellationToken);

        return Ok(vehicles);
    }

    [HttpPatch("{id}/state")]
    public async Task<ActionResult<VehicleDto>> ModifyVehicleState(
        [FromRoute] string id,
        [FromBody] ModifyVehicleStateRequest request,
        CancellationToken cancellationToken)
    {
        var vehicle = await _mediator.Send(new ModifyVehicleStateCommand
        {
            VehicleId = id,
            Status = request.Status
        }, cancellationToken);

        if (vehicle is null)
            return NotFound(new { Message = $"Vehicle '{id}' was not found" });

        return Ok(vehicle);
    }
}
