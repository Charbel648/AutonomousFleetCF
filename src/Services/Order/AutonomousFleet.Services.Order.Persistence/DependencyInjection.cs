using AutonomousFleet.Services.Order.Application.Abstractions.Persistence;
using AutonomousFleet.Services.Order.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutonomousFleet.Services.Order.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("OrderDb")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
