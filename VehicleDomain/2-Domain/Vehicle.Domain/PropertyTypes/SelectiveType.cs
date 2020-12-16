using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Vehicle.Domain.VehiclePositions;
using System.Linq;
using Framework.Domain.Enum;
using Framework.Application;

namespace Vehicle.Domain.PropertyTypes
{
    public class SelectiveType : PropertyType
    {
        private IList<Option> options;
        public virtual ReadOnlyCollection<Option> Options{ get { return new ReadOnlyCollection<Option>(options); } }
        public SelectiveType(bool required, List<Option> options)
            : base(  required)
        {
            this.options = options;
        }
        private SelectiveType():base()
        {

        }
        public override PropertyValue CreatePropertyValue(int propertyId ,string value)
        {
            if (!this.options.Any(o => o.Text == value))
                throw new ExceptionResult(StatusEnum.InvalidValue);
            return new PropertyValue(propertyId, value);
        }
    }


}