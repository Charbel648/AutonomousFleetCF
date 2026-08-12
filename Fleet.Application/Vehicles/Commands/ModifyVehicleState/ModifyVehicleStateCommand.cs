using System.Text.Json.Serialization;
using Fleet.Application.Vehicles.Dtos;
using Fleet.Domain.Enums;
using MediatR;

namespace Fleet.Application.Vehicles.Commands.ModifyVehicleState;

public class ModifyVehicleStateCommand : IRequest<VehicleDto?>
{
    [JsonIgnore]
    public Guid VehicleId { get; set; }

    [JsonIgnore]
    public string TenantId { get; set; } = string.Empty;

    public VehicleStatus Status { get; set; }
}
