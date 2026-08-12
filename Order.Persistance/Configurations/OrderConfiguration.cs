using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;
using OrderEntity = global::Order.Domain.Entities.Order;

namespace Order.Persistance.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(order => order.OrderId);

        builder.Ignore(order => order.Id);

        builder.Property(order => order.TenantId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.CustomerId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.PickupAddress)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(order => order.DeliveryAddress)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(order => order.AssignedVehicleId);

        builder.Property(order => order.Status)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<OrderStatus>(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(order => order.CreatedAt)
            .IsRequired();

        builder.Property(order => order.LastUpdatedAt)
            .IsRequired();

        builder.HasIndex(order => new { order.TenantId, order.OrderId });
    }
}
