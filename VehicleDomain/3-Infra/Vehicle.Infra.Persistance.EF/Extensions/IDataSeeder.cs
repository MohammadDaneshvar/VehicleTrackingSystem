using Framework.Data.EF;
using Framework.Domain.Repository;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.DynamicProperties;
using Vehicle.Domain.PropertyTypes;
using Vehicle.Domain.VehiclePositions;
using Vehicle.Domain.VehicleProperties;
using Vehicle.Domain.Vehicles;
using Vehicle.Infra.Persistance.EF;

namespace Vehicle.AppService.DataSeeders
{
    public interface IDataSeeder
    {
        Task SeedAsync();

    }
}
