using Order.API.Middleware;
using Order.API.Tenancy;
using Order.API.Validation;
using Order.Application;
using Order.Application.Abstractions.Tenancy;
using Order.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TenantContext>();
builder.Services.AddScoped<ITenantContext>(provider =>
    provider.GetRequiredService<TenantContext>());
builder.Services.AddScoped<ITenantContextSetter>(provider =>
    provider.GetRequiredService<TenantContext>());

builder.Services.AddOrderApplication();
builder.Services.AddOrderPersistance(builder.Configuration);

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
