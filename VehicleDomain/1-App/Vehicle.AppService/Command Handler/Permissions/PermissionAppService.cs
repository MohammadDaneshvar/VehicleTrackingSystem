using Framework.Application;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.AppService.Contracts.Commands.Permissions;
using Vehicle.Domain;

namespace Vehicle.AppService.Command_Handler.Permissions
{
    public class PermissionAppService : ICommandHandler<UpdateRolePermissionCommand>
    {
        private readonly IRepository<RolePermission> rolePermissionRepository;

        public PermissionAppService(IRepository<RolePermission> rolePermissionRepository)
        {
            this.rolePermissionRepository = rolePermissionRepository;
        }
      

        public async Task HandleAsync(UpdateRolePermissionCommand command, CancellationToken cancellationToken)
        {
            List<RolePermission> old = await rolePermissionRepository.Where(x => x.RoleId == command.RoleId).ToListAsync();
            if (command.PermissionsId?.Count <= 0)
            {
                if (old.Count > 0)
                {
                    rolePermissionRepository.RemoveRange(old);
                }
            }
            else
            {
                var oldPermissionId = old.Select(x => x.PermissionId);
                List<RolePermission> added = command.PermissionsId.Where(x => !oldPermissionId.Contains(x)).Select(x => new RolePermission( command.RoleId ,x)).ToList();
                List<RolePermission> remove = old.Where(x => !command.PermissionsId.Contains(x.PermissionId)).ToList();
                rolePermissionRepository.RemoveRange(remove);
                await rolePermissionRepository.AddRangeAsync(added);
            }
        }
    }
}
