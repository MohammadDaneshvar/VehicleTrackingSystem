using System;

namespace Vehicle.AppService.Contracts
{
    
    public class CommandRouteAttribute : Attribute
    {
        public string Route { get; }
        public CommandRouteAttribute(string route)
        {
            Route = route;
        }

        
    }
}