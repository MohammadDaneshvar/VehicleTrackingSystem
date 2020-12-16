using Framework.Data.EF;
using Framework.Domain.Domain;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain;
using Vehicle.Domain.DynamicProperties;
using Vehicle.Domain.Permissions;
using Vehicle.Domain.PropertyTypes;
using Vehicle.Domain.VehiclePositions;
using Vehicle.Domain.VehicleProperties;
using Vehicle.Domain.Vehicles;

namespace Vehicle.AppService.DataSeeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly IRepository<PropertyType> _propertyTypeRepository;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IPropertyValidator _propertyValidator;
        private readonly IRepository<VehicleDomain> _vehicleDomainRepository;
        private readonly IRepository<VehiclePosition> _vehiclePositionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DataSeeder(IRepository<PropertyType> propertyTypeRepository, IRepository<Property> propertyRepository,
            IPropertyValidator propertyValidator, IRepository<VehicleDomain> VehicleDomainRepository,
            IRepository<VehiclePosition> vehiclePositionRepository, 
            IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository, IRepository<RolePermission> rolePermissionRepository

            , IUnitOfWork unitOfWork
            )
        {
            this._propertyTypeRepository = propertyTypeRepository;
            this._propertyRepository = propertyRepository;
            this._propertyValidator = propertyValidator;
            _vehicleDomainRepository = VehicleDomainRepository;
            this._vehiclePositionRepository = vehiclePositionRepository;
            this._unitOfWork = unitOfWork;
        }



        public async Task SeedAsync()
        {
            if (!(await _vehicleDomainRepository.Query().AnyAsync()))
            {
                var vehicle = new VehicleDomain(2515,100000548, "<DSAKeyValue><P>ykJeBgyXzpXuMMtr6wGXNhrXLQJoVzZ0WJ/T1YfBprugSXy2tQqJNd7IHXy2S7KO2v4LQEnHSNI1rV5ADLt7FPtjBMPB6Ld4exIU/0YLAWFLdd6dE38KZoWGRfkJhYH3H+bTTDWjbyx6yb0moqqW/ZXhqNCWd/hnMqsA5VgLxvs=</P><Q>wSwVkmOHZhQecH1sGjr2JpeopPU=</Q><G>BGwzjRGzebPav2b3adnCE8uzBouLWnDy9n2qjoaEY7liRz7L+lmRfYubY+0RziOXSqV7MHoAgd0woo/AaUZAbrKOvAv/5w0kswtwHwJLALv1ajqie1BAVtuxjuhwdkiMn6LNpp/z4W143WugDXsyjTEdsGe/MQcmLp4DntR8TU8=</G><Y>um1g9my6M900dknMZZYbLhmdwtPODxAXsXKIaoRO7dA4Lg9Bb2/f+5gn3Ti2TZxH5chPFps8XR6csUkP9jGKs0KxzhaZnOb8TQ8O4Fr2W5cj4CWhMo/jiOiMBxWY6zDYzIpDBoStyjHdf2gATxOCZbEp+uA/qPcCCnOl8/NIHio=</Y></DSAKeyValue>");
                await _vehicleDomainRepository.AddAsync(vehicle);

                var fuelType = new TextType(false);
                var SpeedType = new SelectiveType(false, new List<Option>
            {
                new Option("0"),
                new Option("50"),
                new Option("100"),
            });


                await _propertyRepository.AddRangeAsync(new List<Property>
            {
                new Property(1, "Fuel",fuelType.Id, _propertyValidator),
                new Property(2, "Speed",SpeedType.Id, _propertyValidator)
            });



                await _propertyTypeRepository.AddRangeAsync(new List<PropertyType>
            {
                fuelType,
                SpeedType
            });


                var propertyValues = new List<PropertyValue>
            {
               fuelType.CreatePropertyValue(1,"80"),
               SpeedType.CreatePropertyValue(2,"50"),
              };

                

                //https://www.google.com/maps/@35.8275336,51.2632353,12z
                var point = new VehiclePoint(13.003725d, 55.604870d,17) ;
                var VehiclePosition = new VehiclePosition(1, point, propertyValues);
                await _vehiclePositionRepository.AddAsync(VehiclePosition);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
