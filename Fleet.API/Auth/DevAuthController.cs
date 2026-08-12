using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fleet.API.Auth;

[ApiController]
[Route("auth")]
public class DevAuthController : ControllerBase
{
    private readonly JwtOptions _jwtOptions;
    private readonly IWebHostEnvironment _environment;

    public DevAuthController(
        IOptions<JwtOptions> jwtOptions,
        IWebHostEnvironment environment)
    {
        _jwtOptions = jwtOptions.Value;
        _environment = environment;
    }

    [AllowAnonymous]
    [HttpPost("dev-token")]
    public ActionResult CreateToken([FromBody] DevLoginRequest request)
    {
        if (!_environment.IsDevelopment())
            return NotFound();

        if (request.UserId != "charbel.admin"
            || request.Password != "Admin@2026"
            || request.TenantId != "tenant-a")
        {
            return Unauthorized(new
            {
                Message = "Invalid development credentials"
            });
        }

        var role = SystemRoles.Admin;

        var permissions = new List<string>
        {
            PermissionNames.CanRegisterVehicle,
            PermissionNames.CanReadVehicles,
            PermissionNames.CanModifyVehicleState,
            PermissionNames.CanDeleteVehicle
        };

        var claims = new List<Claim>
        {
            new(JwtClaimTypes.Subject, request.UserId),
            new(JwtClaimTypes.TenantId, request.TenantId),
            new(JwtClaimTypes.Role, role)
        };

        foreach (var permission in permissions)
        {
            claims.Add(new Claim(JwtClaimTypes.Permission, permission));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresAt = expiresAt,
            UserId = request.UserId,
            TenantId = request.TenantId,
            Role = role,
            Permissions = permissions
        });
    }
}

public class DevLoginRequest
{
    public string UserId { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string TenantId { get; set; } = string.Empty;
}
