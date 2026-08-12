namespace Order.Domain.Enums;

public enum OrderStatus
{
    Created = 1,
    Queued = 2,
    Assigned = 3,
    Running = 4,
    Completed = 5,
    Failed = 6,
    Cancelled = 7
}
