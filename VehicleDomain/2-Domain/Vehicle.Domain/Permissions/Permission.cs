//using Framework.Application;
//using Framework.Domain.Aggregate;
//using StackExchange.Redis;
//using System.Collections.Generic;

//namespace Vehicle.Domain
//{
//    public class Permission:AggregateRoot<CommandPermission>
//    {
//        private CommandPermission id;
//        public override CommandPermission Id { get { return Id; } }
//        public string Name { get; private set; }
//        public string Description { get; private set; }
//        private Permission()
//        {

//        }
//        public Permission(CommandPermission id, string Name, string Description)
//        {
//            this.id = id;
//            this.Name = Name;
//            this.Description = Description;
//        }

//    }
//}
