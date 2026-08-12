using Fleet.Domain.Entities;
using Fleet.Domain.Enums;
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
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(vehicle => vehicle.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vehicle => vehicle.Status)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<VehicleStatus>(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vehicle => vehicle.BatteryLevel)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(vehicle => vehicle.Latitude)
            .HasPrecision(10, 7)
            .IsRequired();

        builder.Property(vehicle => vehicle.Longitude)
            .HasPrecision(10, 7)
            .IsRequired();

        builder.Property(vehicle => vehicle.LastTelemetryAt)
            .IsRequired();

        builder.Property(vehicle => vehicle.CreatedAt)
            .IsRequired();

        builder.Property(vehicle => vehicle.LastUpdatedAt)
            .IsRequired();

        builder.HasIndex(vehicle => new { vehicle.TenantId, vehicle.VehicleId });

        builder.HasIndex(vehicle => new { vehicle.TenantId, vehicle.RegistrationNumber })
            .IsUnique();
    }
}
