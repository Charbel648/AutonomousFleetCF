using Order.Application.Abstractions.Tenancy;

namespace Order.API.Middleware;

public class TenantMiddleware
{
    private const string TenantHeaderName = "X-Tenant-Id";
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ITenantContextSetter tenantContextSetter)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(TenantHeaderName, out var tenantHeader)
            || string.IsNullOrWhiteSpace(tenantHeader))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Missing X-Tenant-Id header"
            });

            return;
        }

        tenantContextSetter.SetTenantId(tenantHeader.ToString());

        await _next(context);
    }
}
