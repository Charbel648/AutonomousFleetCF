using AutonomousFleet.Services.Fleet.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Fleet.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutonomousFleet.Services.Fleet.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddFleetPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FleetDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FleetDb")));

        services.AddScoped<IVehicleRepository, VehicleRepository>();

        return services;
    }
}
