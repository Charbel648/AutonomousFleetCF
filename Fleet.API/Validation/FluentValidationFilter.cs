using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fleet.API.Validation;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            PopulateRouteIdIfNeeded(context, argument);

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator is null)
                continue;

            var validationContext = new ValidationContext<object>(argument);
            var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

            if (!validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(error => new
                    {
                        error.PropertyName,
                        error.ErrorMessage
                    })
                });

                return;
            }
        }

        await next();
    }

    private static void PopulateRouteIdIfNeeded(ActionExecutingContext context, object argument)
    {
        if (!context.RouteData.Values.TryGetValue("id", out var routeValue))
            return;

        if (!Guid.TryParse(routeValue?.ToString(), out var id))
            return;

        var property = argument.GetType().GetProperty("VehicleId");

        if (property is null || property.PropertyType != typeof(Guid))
            return;

        var currentValue = (Guid)(property.GetValue(argument) ?? Guid.Empty);

        if (currentValue == Guid.Empty)
            property.SetValue(argument, id);
    }
}
