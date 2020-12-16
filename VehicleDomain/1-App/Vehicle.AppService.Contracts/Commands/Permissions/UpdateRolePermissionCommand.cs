using Framework.Application;
using Framework.Domain.Enum;
using System.Collections.Generic;

namespace Vehicle.AppService.Contracts.Commands.Permissions
{
    [CommandRoute("admin/UpdateRolePermission")]
    public class UpdateRolePermissionCommand : IRestrictedCommand
    {
        public int RoleId { get; set; }
        public List<int> PermissionsId { get; set; }
    }
}
