using FluentValidation;

namespace Fleet.Application.Vehicles.Commands.ModifyVehicleState;

public class ModifyVehicleStateCommandValidator : AbstractValidator<ModifyVehicleStateCommand>
{
    public ModifyVehicleStateCommandValidator()
    {
        RuleFor(command => command.VehicleId)
            .NotEmpty();

        RuleFor(command => command.Status)
            .IsInEnum();
    }
}
