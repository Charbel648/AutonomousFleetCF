using Fleet.API.Middleware;
using Fleet.API.Tenancy;
using Fleet.API.Validation;
using Fleet.Application;
using Fleet.Application.Abstractions.Tenancy;
using Fleet.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TenantContext>();
builder.Services.AddScoped<ITenantContext>(provider =>
    provider.GetRequiredService<TenantContext>());
builder.Services.AddScoped<ITenantContextSetter>(provider =>
    provider.GetRequiredService<TenantContext>());

builder.Services.AddFleetApplication();
builder.Services.AddFleetPersistance(builder.Configuration);

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
