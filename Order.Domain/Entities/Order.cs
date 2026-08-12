using Order.Domain.Enums;

namespace Order.Domain.Entities;

public class Order
{
    public Guid OrderId { get; private set; }

    public Guid Id => OrderId;

    public string TenantId { get; private set; } = string.Empty;

    public string CustomerId { get; private set; } = string.Empty;

    public string PickupAddress { get; private set; } = string.Empty;

    public string DeliveryAddress { get; private set; } = string.Empty;

    public Guid? AssignedVehicleId { get; private set; }

    public OrderStatus Status { get; private set; }

    public int PriorityScore { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastUpdatedAt { get; private set; }

    public DateTime? QueuedAt { get; private set; }

    public DateTime? AssignedAt { get; private set; }

    public DateTime? StartedAt { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public DateTime? FailedAt { get; private set; }

    public DateTime? CancelledAt { get; private set; }

    private Order()
    {
    }

    public Order(
        string tenantId,
        string customerId,
        string pickupAddress,
        string deliveryAddress,
        int priorityScore = 0)
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new ArgumentException("Tenant id is required", nameof(tenantId));

        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer id is required", nameof(customerId));

        if (string.IsNullOrWhiteSpace(pickupAddress))
            throw new ArgumentException("Pickup address is required", nameof(pickupAddress));

        if (string.IsNullOrWhiteSpace(deliveryAddress))
            throw new ArgumentException("Delivery address is required", nameof(deliveryAddress));

        ValidatePriorityScore(priorityScore);

        OrderId = Guid.NewGuid();
        TenantId = tenantId.Trim();
        CustomerId = customerId.Trim();
        PickupAddress = pickupAddress.Trim();
        DeliveryAddress = deliveryAddress.Trim();
        PriorityScore = priorityScore;
        Status = OrderStatus.Created;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = CreatedAt;
    }

    public void Queue()
    {
        EnsureStatus(OrderStatus.Created);

        Status = OrderStatus.Queued;
        QueuedAt = DateTime.UtcNow;

        Touch();
    }

    public void AssignVehicle(Guid vehicleId)
    {
        if (vehicleId == Guid.Empty)
            throw new ArgumentException("Vehicle id is required", nameof(vehicleId));

        if (Status != OrderStatus.Created && Status != OrderStatus.Queued)
            throw new InvalidOperationException("Only created or queued orders can be assigned");

        AssignedVehicleId = vehicleId;
        Status = OrderStatus.Assigned;
        AssignedAt = DateTime.UtcNow;

        Touch();
    }

    public void Start()
    {
        EnsureStatus(OrderStatus.Assigned);

        Status = OrderStatus.Running;
        StartedAt = DateTime.UtcNow;

        Touch();
    }

    public void Complete()
    {
        EnsureStatus(OrderStatus.Running);

        Status = OrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;

        Touch();
    }

    public void Fail()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Completed or cancelled orders cannot fail");

        Status = OrderStatus.Failed;
        FailedAt = DateTime.UtcNow;

        Touch();
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Failed)
            throw new InvalidOperationException("Completed or failed orders cannot be cancelled");

        Status = OrderStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;

        Touch();
    }

    public void ChangePriorityScore(int priorityScore)
    {
        ValidatePriorityScore(priorityScore);

        PriorityScore = priorityScore;

        Touch();
    }

    private static void ValidatePriorityScore(int priorityScore)
    {
        if (priorityScore < 0 || priorityScore > 100)
            throw new ArgumentOutOfRangeException(
                nameof(priorityScore),
                "Priority score must be between 0 and 100");
    }

    private void EnsureStatus(OrderStatus expectedStatus)
    {
        if (Status != expectedStatus)
            throw new InvalidOperationException(
                $"Order must be {expectedStatus} but current status is {Status}");
    }

    private void Touch()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}
