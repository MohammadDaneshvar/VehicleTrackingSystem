using Framework.Application.Common.Attributes;
using Framework.Domain.Aggregate;
using Vehicle.Domain.VehicleProperties;

namespace Vehicle.Domain.VehiclePositions
{
    public class PropertyValue : ValueObject
    {
        [SwaggerIgnore]
        public virtual int VehiclePositionId { get; private set; }
        public virtual int PropertyId { get; private set; }
        public  string Value { get; private set; }
        [SwaggerIgnore]

        public  VehiclePosition VehiclePosition { get;private  set; }
        [SwaggerIgnore]
        public Property Property { get; private set; }
        private PropertyValue()
        {

        }
        public PropertyValue(
            int propertyId,
            string value)
        {
            PropertyId = propertyId;
            Value = value;
        }

    }
}