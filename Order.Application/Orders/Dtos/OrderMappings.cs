using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Application.Orders.Dtos;

public static class OrderMappings
{
    public static OrderDto ToDto(this OrderEntity order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            PickupAddress = order.PickupAddress,
            DeliveryAddress = order.DeliveryAddress,
            AssignedVehicleId = order.AssignedVehicleId,
            Status = order.Status.ToString(),
            PriorityScore = order.PriorityScore,
            CreatedAt = order.CreatedAt,
            LastUpdatedAt = order.LastUpdatedAt
        };
    }
}
