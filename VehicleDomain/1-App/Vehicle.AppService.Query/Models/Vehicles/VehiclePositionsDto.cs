using Framework.Domain.Domain;
using System;

namespace Vehicle.AppService.Query.Models.Vehicles
{
    public class VehiclePositionsDto
    {
        public VehiclePoint Point { get; set; }
        public string LocalityName { get; set; }
        public DateTime Date { get; set; }
    }
}
