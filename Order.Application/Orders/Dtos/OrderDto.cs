using Order.Domain.Enums;

namespace Order.Application.Orders.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }

    public string TenantId { get; set; } = string.Empty;

    public string CustomerId { get; set; } = string.Empty;

    public string PickupAddress { get; set; } = string.Empty;

    public string DeliveryAddress { get; set; } = string.Empty;

    public Guid? AssignedVehicleId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
