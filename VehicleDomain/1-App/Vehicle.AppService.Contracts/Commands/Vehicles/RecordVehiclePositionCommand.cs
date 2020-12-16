using Framework.Application;
using Framework.Domain.Domain;
using System.Collections.Generic;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.AppService.Contracts.Commands.Vehicles
{
    public class RecordVehiclePositionCommand : IRestrictedCommand
    {
        public int VehicleId { get;  set; }
        public VehiclePoint Location { get;  set; }
        public string DigitalSignature { get; set; }
        public IList<PropertyValue> PropertyValues { get;  set; }
    }
}
