using FluentValidation;
using Framework.Application;
using Vehicle.AppService.Contracts.Resources;

namespace Vehicle.AppService.Contracts.Commands.Permissions
{
    public class UpdateRolePermissionCommandValidation : AbstractValidator<UpdateRolePermissionCommand>, ICommandValidator<UpdateRolePermissionCommand>
    {
        public UpdateRolePermissionCommandValidation()
        {
            RuleFor(x => x.RoleId).NotNull().WithMessage(ValidationMessage.Role).GreaterThan(0).WithMessage(ValidationMessage.Role);
        }
    }
}
