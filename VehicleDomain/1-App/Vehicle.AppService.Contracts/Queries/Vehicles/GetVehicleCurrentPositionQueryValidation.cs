using FluentValidation;
using Framework.Application;

namespace Vehicle.AppService.Contracts.Queries.Vehicles
{
    public class GetVehicleCurrentPositionQueryValidation : AbstractValidator<GetVehicleCurrentPositionQuery>, ICommandValidator<GetVehicleCurrentPositionQuery>
    {
        public GetVehicleCurrentPositionQueryValidation()
        {
        }
    }
}
