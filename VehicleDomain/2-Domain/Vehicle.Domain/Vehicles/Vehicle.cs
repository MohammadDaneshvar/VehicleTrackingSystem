using Framework.Domain.Aggregate;
using System;
using System.Collections.Generic;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.Domain.Vehicles
{
    public   class VehicleDomain: AggregateRoot<int>
    {
        private  int id { get; set; }
        public override int Id { get { return Id; } }
        public long VehicleECUNumber { get;private set; }
        public long GPSDeviceSerial { get; private set; }
        public string PublicKey { get; private set; }
        public DateTime RegDate { get; private set; }
        private VehicleDomain()
        {

        }
        public VehicleDomain(long vehicleECUNumber , long gPSDeviceSerial, string publicKey)
        {
            this.RegDate = DateTime.Now;
            VehicleECUNumber = vehicleECUNumber;
            GPSDeviceSerial = gPSDeviceSerial;
            PublicKey = publicKey;
        }
    }
}

