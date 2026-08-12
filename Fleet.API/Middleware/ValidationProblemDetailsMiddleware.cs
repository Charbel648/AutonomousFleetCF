using Fleet.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Middleware;

public class ValidationProblemDetailsMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationProblemDetailsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7807",
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            problemDetails.Extensions["errors"] = exception.Errors;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
