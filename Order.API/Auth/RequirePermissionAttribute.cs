using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Order.API.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public RequirePermissionAttribute(string permission)
    {
        _permission = permission;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Message = "Authentication is required"
            });

            return Task.CompletedTask;
        }

        var isAdmin = user.IsInRole(SystemRoles.Admin);
        var hasPermission = user.HasClaim(JwtClaimTypes.Permission, _permission);

        if (!isAdmin && !hasPermission)
        {
            context.Result = new ForbidResult();
            return Task.CompletedTask;
        }

        if (context.HttpContext.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader))
        {
            var tenantFromHeader = tenantHeader.ToString().Trim();
            var tenantFromToken = user.FindFirst(JwtClaimTypes.TenantId)?.Value;

            if (string.IsNullOrWhiteSpace(tenantFromToken)
                || !string.Equals(tenantFromHeader, tenantFromToken, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new ForbidResult();
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }
}
