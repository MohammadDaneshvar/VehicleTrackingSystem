using Framework.Application;
using Framework.Application.Common.Attributes;
using System;
using System.Collections.Generic;
using Vehicle.AppService.Query.Models.Vehicles;

namespace Vehicle.AppService.Contracts.Queries.Vehicles
{
    public class GetVehiclePositionsQuery : IRestrictedCommand, IHaveResult<List<VehiclePositionsDto>>
    {
        public int VehicleId { get; set; }
        public DateTime  StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [SwaggerIgnore]
        public List<VehiclePositionsDto> Result { get; set; }

    }
}
