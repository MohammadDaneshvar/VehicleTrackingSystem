using Framework.Application;
using Framework.Domain.Aggregate;
using StackExchange.Redis;

namespace Vehicle.Domain
{

    public class RolePermission:AggregateRoot<int>
    {
        private int id;
        public override int Id { get { return id; } }
        
        public int RoleId { get; private set; }
        public CommandPermission PermissionId { get; private set; }
        private RolePermission()
        {

        }
        public RolePermission(int roleId, CommandPermission permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        

        
    }
}
