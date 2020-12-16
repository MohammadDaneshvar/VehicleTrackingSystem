using FluentValidation;
using FluentValidation.Results;
using Framework.Application;

namespace Vehicle.AppService.Contracts.Commands.Vehicles
{
    public class RegisterVehicleCommandValidation : AbstractValidator<RegisterVehicleCommand>, ICommandValidator<RegisterVehicleCommand>
    {
        public ValidationResult Validate(RegisterVehicleCommand command)
        {
            return new ValidationResult();
        }
    }
}
