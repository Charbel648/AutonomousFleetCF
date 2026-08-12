using FluentValidation;
using MediatR;
using OrderValidationException = Order.Application.Common.Exceptions.ValidationException;

namespace Order.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validators = _validators.ToList();

        if (validators.Count == 0)
            return await next();

        var validationContext = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(validator =>
                validator.ValidateAsync(validationContext, cancellationToken)));

        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(error => error.ErrorMessage)
                    .Distinct()
                    .ToArray());

        if (errors.Count > 0)
            throw new OrderValidationException(errors);

        return await next();
    }
}
