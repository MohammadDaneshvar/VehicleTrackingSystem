using Vehicle.AppService.Contracts.Queries.Permissions;
using Framework.Application;
using Framework.Application.Common.Extentions;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Domain;

namespace Vehicle.AppService.Query_Handler.Permissions
{
    public class PermissionQueryHandlers : ICommandHandler<GetAllPermissionsByRoleQuery>
    {
        private readonly IRepository<RolePermission> rolePermissionRepository;

        public PermissionQueryHandlers(IRepository<RolePermission> rolePermissionRepository)
        {
            this.rolePermissionRepository = rolePermissionRepository;
        }

        public async Task HandleAsync(GetAllPermissionsByRoleQuery command, CancellationToken cancellationToken)
        {
            //var PermissionsId = await rolePermissionRepository.Where(x => x.RoleId == command.RoleId).Select(x => x.PermissionId).ToListAsync();
            //if (PermissionsId.Count>0)
            //{
            //    ActionTypeEnum[] actionTypes = (ActionTypeEnum[])Enum.GetValues(typeof(ActionTypeEnum));

            //    command.Result = actionTypes.Where(x=> PermissionsId.Contains((int)x)).Select(x => new Permission()
            //    {
            //        PermissionId = (int)x,
            //        Name = x.ToString(),
            //        FaName = EnumHelper.GetDisplayName(x),
            //        Description = EnumHelper.GetDescription(x),
            //    }).ToList();
        }
    }
}
