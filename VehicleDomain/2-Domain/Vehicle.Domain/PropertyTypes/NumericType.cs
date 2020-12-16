using System;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.Domain.PropertyTypes
{
    public class NumericType : PropertyType
    {
        private long min;
        public virtual long Min { get { return min; } }

        private long max;
        public virtual long Max { get { return max; } }
        public NumericType(bool required, long min, long max)
            : base( required)
        {
            this.min = min;
            this.max = max;
        }

        public override PropertyValue CreatePropertyValue(int propertyId ,string value)
        {
            long result = 0;
            if (!long.TryParse(value, out result))
                throw new ArgumentOutOfRangeException();
            if (result < min)
                throw new ArgumentOutOfRangeException();
            if (result > max)
                throw new ArgumentOutOfRangeException();
            return new PropertyValue(propertyId, value);
        }
    }


}