using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstractions.Persistence;
using Order.Persistance.Repositories;

namespace Order.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderPersistance(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("OrderDb")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
