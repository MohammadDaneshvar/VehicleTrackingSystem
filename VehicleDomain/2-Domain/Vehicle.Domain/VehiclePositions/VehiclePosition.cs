using Framework.Domain.Aggregate;
using Framework.Domain.Domain;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace Vehicle.Domain.VehiclePositions
{
    public class VehiclePosition : AggregateRoot<int>
    {
        private int id { get; set; }
        public override int Id { get { return Id; } }

        public  virtual int VehicleId { get; private set; }
        public Point Point { get; private  set; }
        public IList<PropertyValue> propertyValues { get; private set; }
        public DateTime RegDateTime { get; private set; }
        private VehiclePosition() { } // Needed by ORM

        public VehiclePosition(int vehicleId, VehiclePoint point, IList<PropertyValue> propertyValues)
        {
            VehicleId = vehicleId;
            Point =new Point(point.X, point.Y) { SRID =point.SRID  }; //4326
            this.propertyValues = propertyValues;
            RegDateTime = DateTime.Now;
        }
    }
}
