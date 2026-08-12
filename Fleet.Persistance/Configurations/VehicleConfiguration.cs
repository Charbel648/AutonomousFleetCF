using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Persistance.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(vehicle => vehicle.VehicleId);

        builder.Ignore(vehicle => vehicle.Id);

        builder.Property(vehicle => vehicle.TenantId)
            .HasColumnName("tenant_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(vehicle => vehicle.RegistrationNumber)
            .HasColumnName("registration_number")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(vehicle => vehicle.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(vehicle => vehicle.BatteryLevel)
            .HasColumnName("battery_level")
            .IsRequired();

        builder.Property(vehicle => vehicle.Latitude)
            .HasColumnName("latitude")
            .HasPrecision(9, 6)
            .IsRequired();

        builder.Property(vehicle => vehicle.Longitude)
            .HasColumnName("longitude")
            .HasPrecision(9, 6)
            .IsRequired();

        builder.Property(vehicle => vehicle.TelemetryData)
            .HasColumnName("telemetry_data")
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'{}'::jsonb")
            .IsRequired();

        builder.Property(vehicle => vehicle.LastTelemetryAt)
            .HasColumnName("last_telemetry_at")
            .IsRequired();

        builder.Property(vehicle => vehicle.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(vehicle => vehicle.LastUpdatedAt)
            .HasColumnName("last_updated_at")
            .IsRequired();

        builder.HasIndex(vehicle => new
        {
            vehicle.TenantId,
            vehicle.RegistrationNumber
        }).IsUnique();
    }
}
