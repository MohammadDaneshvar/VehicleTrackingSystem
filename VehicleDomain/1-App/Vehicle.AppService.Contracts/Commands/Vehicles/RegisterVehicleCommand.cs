using Framework.Application;
using System;

namespace Vehicle.AppService.Contracts.Commands.Vehicles
{
    public class RegisterVehicleCommand : IRestrictedCommand
    {
        public long VehicleECUNumber { get;  set; }
        public long GPSDeviceSerial { get;  set; }
        public string PublicKey { get; set; }

    }
}
