using System.Text.Json;
using FluentValidation;

namespace Fleet.Application.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommandValidator : AbstractValidator<RegisterVehicleCommand>
{
    public RegisterVehicleCommandValidator()
    {
        RuleFor(command => command.RegistrationNumber)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.BatteryLevel)
            .InclusiveBetween(0, 100);

        RuleFor(command => command.Latitude)
            .InclusiveBetween(-90, 90);

        RuleFor(command => command.Longitude)
            .InclusiveBetween(-180, 180);

        RuleFor(command => command.TelemetryData)
            .NotEmpty()
            .Must(BeValidJson)
            .WithMessage("TelemetryData must contain valid JSON");
    }

    private static bool BeValidJson(string value)
    {
        try
        {
            JsonDocument.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
