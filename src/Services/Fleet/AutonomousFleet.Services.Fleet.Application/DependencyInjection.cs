using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AutonomousFleet.Services.Fleet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddFleetApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
