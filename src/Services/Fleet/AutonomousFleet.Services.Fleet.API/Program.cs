using AutonomousFleet.Services.Fleet.API.Middleware;
using AutonomousFleet.Services.Fleet.API.Tenancy;
using AutonomousFleet.Services.Fleet.API.Validation;
using AutonomousFleet.Services.Fleet.Application;
using AutonomousFleet.Services.Fleet.Application.Abstractions.Tenancy;
using AutonomousFleet.Services.Fleet.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TenantContext>();
builder.Services.AddScoped<ITenantContext>(provider =>
    provider.GetRequiredService<TenantContext>());
builder.Services.AddScoped<ITenantContextSetter>(provider =>
    provider.GetRequiredService<TenantContext>());

builder.Services.AddFleetApplication();
builder.Services.AddFleetPersistence(builder.Configuration);

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
