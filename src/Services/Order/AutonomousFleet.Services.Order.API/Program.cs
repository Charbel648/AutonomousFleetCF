using AutonomousFleet.Services.Order.API.Middleware;
using AutonomousFleet.Services.Order.API.Tenancy;
using AutonomousFleet.Services.Order.API.Validation;
using AutonomousFleet.Services.Order.Application;
using AutonomousFleet.Services.Order.Application.Abstractions.Tenancy;
using AutonomousFleet.Services.Order.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TenantContext>();
builder.Services.AddScoped<ITenantContext>(provider =>
    provider.GetRequiredService<TenantContext>());
builder.Services.AddScoped<ITenantContextSetter>(provider =>
    provider.GetRequiredService<TenantContext>());

builder.Services.AddOrderApplication();
builder.Services.AddOrderPersistence(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{
}
