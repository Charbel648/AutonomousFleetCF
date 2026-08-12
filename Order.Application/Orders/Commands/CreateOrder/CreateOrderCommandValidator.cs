using FluentValidation;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.PickupAddress)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(command => command.DeliveryAddress)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(command => command.PriorityScore)
            .InclusiveBetween(0, 100);
    }
}
