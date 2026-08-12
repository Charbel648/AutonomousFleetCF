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

    public DevAuthController(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    [AllowAnonymous]
    [HttpPost("dev-token")]
    public ActionResult CreateToken([FromBody] DevTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserId))
            return BadRequest(new { Message = "UserId is required" });

        if (string.IsNullOrWhiteSpace(request.TenantId))
            return BadRequest(new { Message = "TenantId is required" });

        if (string.IsNullOrWhiteSpace(request.Role))
            request.Role = SystemRoles.Operator;

        var permissions = request.Permissions.Count > 0
            ? request.Permissions
            : GetDefaultPermissions(request.Role);

        var claims = new List<Claim>
        {
            new(JwtClaimTypes.Subject, request.UserId),
            new(JwtClaimTypes.TenantId, request.TenantId),
            new(JwtClaimTypes.Role, request.Role)
        };

        foreach (var permission in permissions.Distinct())
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
            TenantId = request.TenantId,
            Role = request.Role,
            Permissions = permissions
        });
    }

    private static List<string> GetDefaultPermissions(string role)
    {
        if (role == SystemRoles.Admin)
        {
            return new List<string>
            {
                PermissionNames.CanRegisterVehicle,
                PermissionNames.CanReadVehicles,
                PermissionNames.CanModifyVehicleState,
                PermissionNames.CanDeleteVehicle
            };
        }

        if (role == SystemRoles.Supervisor)
        {
            return new List<string>
            {
                PermissionNames.CanReadVehicles
            };
        }

        return new List<string>
        {
            PermissionNames.CanRegisterVehicle,
            PermissionNames.CanReadVehicles,
            PermissionNames.CanModifyVehicleState
        };
    }
}

public class DevTokenRequest
{
    public string UserId { get; set; } = "dev-user";

    public string TenantId { get; set; } = "tenant-a";

    public string Role { get; set; } = SystemRoles.Operator;

    public List<string> Permissions { get; set; } = new();
}
