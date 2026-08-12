using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Order.API.Auth;
using Order.API.Middleware;
using Order.API.Tenancy;
using Order.API.Validation;
using Order.Application;
using Order.Application.Abstractions.Tenancy;
using Order.Persistance;

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

var jwtOptions = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtOptions>() ?? throw new InvalidOperationException("Jwt configuration is missing");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),

            NameClaimType = JwtClaimTypes.Subject,
            RoleClaimType = JwtClaimTypes.Role
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PermissionNames.CanCreateOrder, policy =>
        policy.RequireAuthenticatedUser()
            .RequireClaim(JwtClaimTypes.Permission, PermissionNames.CanCreateOrder));

    options.AddPolicy(PermissionNames.CanReadOrders, policy =>
        policy.RequireAuthenticatedUser()
            .RequireClaim(JwtClaimTypes.Permission, PermissionNames.CanReadOrders));
});

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

app.UseAuthentication();

app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
