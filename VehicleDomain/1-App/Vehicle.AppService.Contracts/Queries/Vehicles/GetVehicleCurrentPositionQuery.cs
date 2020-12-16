using Framework.Application;
using Framework.Application.Common.Attributes;
using Vehicle.AppService.Query.Models.Vehicles;
using Vehicle.Domain.Vehicles;

namespace Vehicle.AppService.Contracts.Queries.Vehicles
{

    public class GetVehicleCurrentPositionQuery : IRestrictedCommand, IHaveResult<VehiclePositionsDto>
    {
        public int VehicleId { get; set; }
        [SwaggerIgnore]
        public VehiclePositionsDto Result { get; set; }

    }
   
}
