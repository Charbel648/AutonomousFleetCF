using System.Text.Json.Serialization;
using Fleet.Application.Vehicles.Dtos;
using MediatR;

namespace Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommand : IRequest<VehicleDto>
{
    [JsonIgnore]
    public string TenantId { get; set; } = string.Empty;

    public string RegistrationNumber { get; set; } = string.Empty;

    public decimal BatteryLevel { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
