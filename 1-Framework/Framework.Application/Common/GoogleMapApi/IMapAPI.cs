using Framework.Domain.Domain;
using NetTopologySuite.Geometries;

namespace Framework.Application.Common.GoogleMapApi
{
    public interface IMapAPI
    {
        string GetNearbyPlaceName(VehiclePoint location);

    }
}
