using FluentValidation;
using Framework.Application;
using Vehicle.AppService.Contracts.Resources;

namespace Vehicle.AppService.Contracts.Queries.Permissions
{
    public class GetAllPermissionsByRoleQueryValidation : AbstractValidator<GetAllPermissionsByRoleQuery>, ICommandValidator<GetAllPermissionsByRoleQuery>
    {
        public GetAllPermissionsByRoleQueryValidation()
        {
            RuleFor(x => x.RoleId).NotNull().WithMessage(ValidationMessage.Role).GreaterThan(0).WithMessage(ValidationMessage.Role);
        }
    }
}
