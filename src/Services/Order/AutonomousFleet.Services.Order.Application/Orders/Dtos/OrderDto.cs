using AutonomousFleet.Services.Order.Domain.Enums;

namespace AutonomousFleet.Services.Order.Application.Orders.Dtos;

public class OrderDto
{
    public string Id { get; set; } = string.Empty;

    public string TenantId { get; set; } = string.Empty;

    public string CustomerId { get; set; } = string.Empty;

    public string PickupAddress { get; set; } = string.Empty;

    public string DeliveryAddress { get; set; } = string.Empty;

    public string? AssignedVehicleId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
