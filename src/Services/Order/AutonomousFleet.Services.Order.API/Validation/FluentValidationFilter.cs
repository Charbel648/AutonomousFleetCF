using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutonomousFleet.Services.Order.API.Validation;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        foreach (object? argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            Type validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            object? validatorObject = context.HttpContext.RequestServices.GetService(validatorType);

            if (validatorObject is not IValidator validator)
                continue;

            var validationContext = new ValidationContext<object>(argument);
            var validationResult = await validator.ValidateAsync(
                validationContext,
                context.HttpContext.RequestAborted);

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
}
