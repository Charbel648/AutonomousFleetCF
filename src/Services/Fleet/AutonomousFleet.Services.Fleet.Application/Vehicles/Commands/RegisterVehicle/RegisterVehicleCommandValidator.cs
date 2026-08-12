using FluentValidation;

namespace AutonomousFleet.Services.Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommandValidator : AbstractValidator<RegisterVehicleCommand>
{
    public RegisterVehicleCommandValidator()
    {
        RuleFor(command => command.RegistrationNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(command => command.BatteryLevel)
            .InclusiveBetween(0, 100);

        RuleFor(command => command.Latitude)
            .InclusiveBetween(-90, 90);

        RuleFor(command => command.Longitude)
            .InclusiveBetween(-180, 180);
    }
}
