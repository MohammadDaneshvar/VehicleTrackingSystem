using Framework.Application;
using Framework.Application.Common.GoogleMapApi;
using Framework.Domain.Domain;
using Framework.Domain.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.AppService.Contracts.Queries.Vehicles;
using Vehicle.AppService.Query.Models.Vehicles;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.AppService.Query_Handler.People
{
    public class VehiclesQueryHandlers : ICommandHandler<GetVehicleCurrentPositionQuery>,
        ICommandHandler<GetVehiclePositionsQuery>
    {
        private readonly IRepository<VehiclePosition> _vehiclePositionRepository;
        private readonly IMapAPI _mapAPI;

        public VehiclesQueryHandlers(IRepository<VehiclePosition> vehiclePositionRepository, IMapAPI mapAPI)
        {
            this._vehiclePositionRepository = vehiclePositionRepository;
            this._mapAPI = mapAPI;
        }
        public async Task HandleAsync(GetVehicleCurrentPositionQuery command, CancellationToken cancellationToken)
        {
            var vehiclePosition = _vehiclePositionRepository.Where(x => x.VehicleId == command.VehicleId)
                .Select(x =>
               new
               {
                   x.Point,
                   x.RegDateTime
               }).OrderByDescending(x => x.RegDateTime).FirstOrDefault();

            if (vehiclePosition != null)
            {
                var result = new VehiclePositionsDto();
                result.Date = vehiclePosition.RegDateTime;
                result.Point = new VehiclePoint(vehiclePosition.Point.X, vehiclePosition.Point.Y, vehiclePosition.Point.Z);
                result.LocalityName = _mapAPI.GetNearbyPlaceName(result.Point);
                command.Result = result;
                new VehiclePoint(vehiclePosition.Point.X, vehiclePosition.Point.Y, vehiclePosition.Point.Z);
            }
        }

        public async Task HandleAsync(GetVehiclePositionsQuery command, CancellationToken cancellationToken)
        {
            var vehiclePositions = _vehiclePositionRepository.Where(x => x.VehicleId == command.VehicleId).OrderBy(x => x.RegDateTime)
                .Select(x => new VehiclePositionsDto
                {
                    Date = x.RegDateTime,
                    Point = new VehiclePoint(x.Point.X, x.Point.Y, x.Point.Z)
                }).ToList();
            if (vehiclePositions != null)
                command.Result = vehiclePositions;
        }
    }
}
