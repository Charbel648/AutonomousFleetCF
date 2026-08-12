using Fleet.Application.Abstractions.Persistence;
using Fleet.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddFleetPersistance(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FleetDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FleetDb")));

        services.AddScoped<IVehicleRepository, VehicleRepository>();

        return services;
    }
}
