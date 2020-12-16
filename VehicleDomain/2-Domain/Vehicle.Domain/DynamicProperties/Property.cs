using Framework.Application;
using Framework.Domain.Aggregate;
using Framework.Domain.Enum;
using System;
using System.Collections.Generic;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.Domain.VehicleProperties
{
    public class Property : AggregateRoot<int>
    {
        private int id;
        public override int Id { get { return id; } }
        public virtual string Name { get; private set; }
        public int PropertyTypeId { get; private set; }
        public IPropertyValidator Validator { get; }
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
        private Property()
        {

        }
        public Property(int id, string name,int  propertyTypeId, IPropertyValidator validator)
        {
            this.id = id;
            Name = name;
            PropertyTypeId = propertyTypeId;
            Validator = validator;
            var isDuplication = validator.PropertyIsDuplication(this);
            if (isDuplication)
                throw new ExceptionResult(StatusEnum.DuplicationData);


        }

    }
}
