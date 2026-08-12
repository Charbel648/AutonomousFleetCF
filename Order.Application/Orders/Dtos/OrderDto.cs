namespace Order.Application.Orders.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }

    public string CustomerId { get; set; } = string.Empty;

    public string PickupAddress { get; set; } = string.Empty;

    public string DeliveryAddress { get; set; } = string.Empty;

    public Guid? AssignedVehicleId { get; set; }

    public string Status { get; set; } = string.Empty;

    public int PriorityScore { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
