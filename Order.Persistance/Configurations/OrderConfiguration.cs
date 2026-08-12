using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Persistance.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(order => order.OrderId);

        builder.Ignore(order => order.Id);

        builder.Property(order => order.TenantId)
            .HasColumnName("tenant_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(order => order.CustomerId)
            .HasColumnName("customer_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(order => order.PickupAddress)
            .HasColumnName("pickup_address")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(order => order.DeliveryAddress)
            .HasColumnName("delivery_address")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(order => order.AssignedVehicleId)
            .HasColumnName("assigned_vehicle_id");

        builder.Property(order => order.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(order => order.PriorityScore)
            .HasColumnName("priority_score")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(order => order.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(order => order.LastUpdatedAt)
            .HasColumnName("last_updated_at")
            .IsRequired();

        builder.Property(order => order.QueuedAt)
            .HasColumnName("queued_at");

        builder.Property(order => order.AssignedAt)
            .HasColumnName("assigned_at");

        builder.Property(order => order.StartedAt)
            .HasColumnName("started_at");

        builder.Property(order => order.CompletedAt)
            .HasColumnName("completed_at");

        builder.Property(order => order.FailedAt)
            .HasColumnName("failed_at");

        builder.Property(order => order.CancelledAt)
            .HasColumnName("cancelled_at");

        builder.HasIndex(order => order.TenantId);

        builder.HasIndex(order => new
        {
            order.TenantId,
            order.Status
        });
    }
}
