using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderEntity = AutonomousFleet.Services.Order.Domain.Entities.Order;

namespace AutonomousFleet.Services.Order.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(order => order.OrderId);

        builder.Property(order => order.OrderId)
            .HasMaxLength(64);

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

        builder.Property(order => order.AssignedVehicleId)
            .HasMaxLength(64);

        builder.Property(order => order.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(order => new { order.TenantId, order.OrderId });
    }
}
