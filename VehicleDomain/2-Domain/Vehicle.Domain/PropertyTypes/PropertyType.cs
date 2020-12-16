using Framework.Domain.Aggregate;
using System.Linq;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.Domain.PropertyTypes
{
    //strategy pattern
    public abstract class PropertyType : EntityBase<PropertyType, int>
    {
        private int id;
        public override int Id { get { return id; } }

        //public  int PropertyId { get; private set; }

        public  bool Required { get; private set; }

        protected PropertyType()       { }
        public PropertyType(
//            int propertyId,
bool required)
        {
//            PropertyId = propertyId;
            Required = required;
        }

        abstract public PropertyValue CreatePropertyValue(int propertyId, string value);
    }


}