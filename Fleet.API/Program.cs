using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Fleet.API.Auth;
using Fleet.API.Middleware;
using Fleet.API.Tenancy;
using Fleet.API.Validation;
using Fleet.Application;
using Fleet.Application.Abstractions.Tenancy;
using Fleet.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
    options.AddPolicy(PermissionNames.CanRegisterVehicle, policy =>
        policy.RequireAuthenticatedUser()
            .RequireClaim(JwtClaimTypes.Permission, PermissionNames.CanRegisterVehicle));

    options.AddPolicy(PermissionNames.CanReadVehicles, policy =>
        policy.RequireAuthenticatedUser()
            .RequireClaim(JwtClaimTypes.Permission, PermissionNames.CanReadVehicles));

    options.AddPolicy(PermissionNames.CanModifyVehicleState, policy =>
        policy.RequireAuthenticatedUser()
            .RequireClaim(JwtClaimTypes.Permission, PermissionNames.CanModifyVehicleState));
});

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token only. Do not write Bearer manually."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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

