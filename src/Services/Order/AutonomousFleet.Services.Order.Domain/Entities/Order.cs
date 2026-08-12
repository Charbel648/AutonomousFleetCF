using AutonomousFleet.Services.Order.Domain.Enums;

namespace AutonomousFleet.Services.Order.Domain.Entities;

public class Order
{
    private Order()
    {
    }

    public Order(
        string tenantId,
        string customerId,
        string pickupAddress,
        string deliveryAddress)
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new ArgumentException("Tenant id is required");

        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer id is required");

        if (string.IsNullOrWhiteSpace(pickupAddress))
            throw new ArgumentException("Pickup address is required");

        if (string.IsNullOrWhiteSpace(deliveryAddress))
            throw new ArgumentException("Delivery address is required");

        OrderId = Guid.NewGuid().ToString();
        TenantId = tenantId;
        CustomerId = customerId;
        PickupAddress = pickupAddress;
        DeliveryAddress = deliveryAddress;
        Status = OrderStatus.Created;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public string OrderId { get; private set; } = string.Empty;

    public string Id => OrderId;

    public string TenantId { get; private set; } = string.Empty;

    public string CustomerId { get; private set; } = string.Empty;

    public string PickupAddress { get; private set; } = string.Empty;

    public string DeliveryAddress { get; private set; } = string.Empty;

    public string? AssignedVehicleId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastUpdatedAt { get; private set; }

    public void Queue()
    {
        EnsureStatus(OrderStatus.Created);
        Status = OrderStatus.Queued;
        Touch();
    }

    public void AssignVehicle(string vehicleId)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
            throw new ArgumentException("Vehicle id is required");

        if (Status != OrderStatus.Created && Status != OrderStatus.Queued)
            throw new InvalidOperationException("Only created or queued orders can be assigned");

        AssignedVehicleId = vehicleId;
        Status = OrderStatus.Assigned;
        Touch();
    }

    public void Start()
    {
        EnsureStatus(OrderStatus.Assigned);
        Status = OrderStatus.Running;
        Touch();
    }

    public void Complete()
    {
        EnsureStatus(OrderStatus.Running);
        Status = OrderStatus.Completed;
        Touch();
    }

    public void Fail()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Completed or cancelled orders cannot fail");

        Status = OrderStatus.Failed;
        Touch();
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Running)
            throw new InvalidOperationException("Completed or running orders cannot be cancelled");

        Status = OrderStatus.Cancelled;
        Touch();
    }

    private void EnsureStatus(OrderStatus expectedStatus)
    {
        if (Status != expectedStatus)
            throw new InvalidOperationException($"Order must be {expectedStatus} before this transition");
    }

    private void Touch()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}
