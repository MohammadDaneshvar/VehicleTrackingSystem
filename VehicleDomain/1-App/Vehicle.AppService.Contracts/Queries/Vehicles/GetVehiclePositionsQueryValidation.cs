using FluentValidation;
using Framework.Application;

namespace Vehicle.AppService.Contracts.Queries.Vehicles
{
    public class GetVehiclePositionsQueryValidation : AbstractValidator<GetVehiclePositionsQuery>, ICommandValidator<GetVehiclePositionsQuery>
    {
        
    }
}
