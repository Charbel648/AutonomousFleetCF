using FluentValidation;

namespace AutonomousFleet.Services.Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.PickupAddress)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(command => command.DeliveryAddress)
            .NotEmpty()
            .MaximumLength(250);
    }
}
