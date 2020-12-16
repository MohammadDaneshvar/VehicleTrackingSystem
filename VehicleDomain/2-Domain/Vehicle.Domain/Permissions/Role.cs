using Framework.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Domain.Permissions
{
    public class Role : AggregateRoot<int>
    {
        private int id { get; set; }
        public override int Id { get { return id; } }
        public string Name { get; private set; }
        private Role()   {    }

        public Role(string name)
        {
            Name = name;
        }
    }
}
