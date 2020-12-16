using Framework.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Domain
{
 public   class UserRole :AggregateRoot<int>
    {

        private int id;
        public override int Id { get { return id; } }
        public int UserId { get; private set; }
        public int RoleId { get; private set; }
        private UserRole()
        {

        }
        public UserRole(int userId,int roleId ) 
        {
            UserId = userId;
            RoleId = roleId;
        }

    }
}
