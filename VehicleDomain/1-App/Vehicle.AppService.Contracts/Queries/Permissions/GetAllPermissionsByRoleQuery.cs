using Framework.Application;
using System.Collections.Generic;
using Vehicle.Domain;

namespace Vehicle.AppService.Contracts.Queries.Permissions
{
    [CommandRoute("admin/GetAllPermissionsByRole")]
    public class GetAllPermissionsByRoleQuery : IRestrictedCommand, IHaveResult<List<Permission>>
    {
        public int RoleId { get; set; }
        public List<Permission> Result { get; set; }
    }
}
