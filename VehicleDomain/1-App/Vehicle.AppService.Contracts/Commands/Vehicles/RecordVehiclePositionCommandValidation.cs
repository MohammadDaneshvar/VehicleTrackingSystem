using FluentValidation;
using FluentValidation.Results;
using Framework.Application;
using Framework.Domain.Enum;
using Framework.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.AppService.Contracts.Resources;
using Vehicle.Domain.Vehicles;

namespace Vehicle.AppService.Contracts.Commands.Vehicles
{
    public class RecordVehiclePositionCommandValidation : AbstractValidator<RecordVehiclePositionCommand>, ICommandValidator<RecordVehiclePositionCommand>
    {
        private readonly IRepository<VehicleDomain> _vehicleDomainRepository;
        private readonly ISignatureProvider _signatureProvider;

        public  RecordVehiclePositionCommandValidation(IRepository<VehicleDomain> vehicleDomainRepository,ISignatureProvider signatureProvider)
        {
            _vehicleDomainRepository = vehicleDomainRepository;
            _signatureProvider = signatureProvider;
            
        }

        public async Task<ValidationResult> ValidateAsync(RecordVehiclePositionCommand command)
        {
            var vehicle = await _vehicleDomainRepository.FindAsync(command.VehicleId);
            RuleFor(x => x.DigitalSignature).Must(x =>
                _signatureProvider.ValidateSignature(command, vehicle.PublicKey, x) == true).WithMessage(Error.SignatureIsNotValid);
            return base.Validate(command);
        }
    }
}
