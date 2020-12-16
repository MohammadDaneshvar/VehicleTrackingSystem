using Framework.Application;
using Framework.Domain.Enum;
using Framework.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.VehicleProperties;

namespace Vehicle.Domain.DynamicProperties
{
    public class PropertyValidator : IPropertyValidator
    {
        private readonly IRepository<Property> _propertyRepository;

        public PropertyValidator(IRepository<Property> propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }
        public  bool PropertyIsDuplication(Property property)
        {
            var result = false;
            var entity =  _propertyRepository.FindAsync(property.Id).Result;
            if (entity != null)
                result= true;
            return result;

        }
    }
}
