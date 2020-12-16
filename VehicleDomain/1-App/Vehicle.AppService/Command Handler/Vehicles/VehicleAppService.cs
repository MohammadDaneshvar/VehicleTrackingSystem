using Framework.Application;
using Framework.Domain.Repository;
using NetTopologySuite.Geometries;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.AppService.Contracts.Commands.Vehicles;
using Vehicle.Domain.VehiclePositions;
using Vehicle.Domain.Vehicles;

namespace Vehicle.AppService.Command_Handler.Subscriptions
{
    public class VehicleAppService : ICommandHandler<RegisterVehicleCommand>,
        ICommandHandler<RecordVehiclePositionCommand>

    {
        private readonly IRepository<VehicleDomain> vehicleRepository;
        private readonly IRepository<VehiclePosition> vehiclePositionRepository;
        private readonly IKeyGenerator keyGenerator;

        public VehicleAppService(IRepository<VehicleDomain> vehicleRepository, IRepository<VehiclePosition> vehiclePositionRepository,
                IKeyGenerator keyGenerator)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehiclePositionRepository = vehiclePositionRepository;
            this.keyGenerator = keyGenerator;
        }
        public async Task HandleAsync(RegisterVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicle = new VehicleDomain(command.VehicleECUNumber,command.GPSDeviceSerial,  command.PublicKey  );
            await vehicleRepository.AddAsync(vehicle);
        }

        public async Task HandleAsync(RecordVehiclePositionCommand command, CancellationToken cancellationToken)
        {
            var vehiclePosition = new VehiclePosition(command.VehicleId,command.Location,command.PropertyValues);
            await vehiclePositionRepository.AddAsync(vehiclePosition);
        }
    }

}