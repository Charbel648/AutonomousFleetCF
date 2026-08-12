using AutonomousFleet.Services.Order.Application.Orders.Dtos;
using AutonomousFleet.Services.Order.Domain.Entities;

namespace AutonomousFleet.Services.Order.Application.Orders.Dtos;

public static class OrderMappings
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            TenantId = order.TenantId,
            CustomerId = order.CustomerId,
            PickupAddress = order.PickupAddress,
            DeliveryAddress = order.DeliveryAddress,
            AssignedVehicleId = order.AssignedVehicleId,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            LastUpdatedAt = order.LastUpdatedAt
        };
    }
}
