using AutonomousFleet.Services.Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomousFleet.Services.Fleet.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(vehicle => vehicle.VehicleId);

        builder.Property(vehicle => vehicle.VehicleId)
            .HasMaxLength(64);

        builder.Property(vehicle => vehicle.TenantId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(vehicle => vehicle.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vehicle => vehicle.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vehicle => vehicle.BatteryLevel)
            .HasPrecision(5, 2);

        builder.Property(vehicle => vehicle.Latitude)
            .HasPrecision(10, 7);

        builder.Property(vehicle => vehicle.Longitude)
            .HasPrecision(10, 7);

        builder.HasIndex(vehicle => new { vehicle.TenantId, vehicle.VehicleId });
        builder.HasIndex(vehicle => new { vehicle.TenantId, vehicle.RegistrationNumber })
            .IsUnique();
    }
}
