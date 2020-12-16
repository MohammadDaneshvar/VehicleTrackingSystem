using Vehicle.Domain.VehiclePositions;

namespace Vehicle.Domain.PropertyTypes
{
    public class TextType : PropertyType
    {
      
        public TextType(bool required)
            : base( required)
        {
        }
        public override PropertyValue CreatePropertyValue(int propertyId, string value)
        {
            return new PropertyValue(propertyId, value);
        }
    }


}