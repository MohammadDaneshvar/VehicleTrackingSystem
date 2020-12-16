using Framework.Domain.Domain;
using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.Places;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Application.Common.GoogleMapApi
{
 public class GoogleMapApi:IMapAPI
    {
        public string GetNearbyPlaceName(VehiclePoint point)
        {
            var result = "";
            GoogleSigned VehicleApiKey = new GoogleSigned("AIzaSyC_iHzls7OqrC2d0OVUERemIyKS8h9BTUE");
            var service = new PlacesService(VehicleApiKey);
            PlacesRequest request = new NearbySearchRequest()
            {
                Location = new LatLng(point.X,point.Y),
            };
            PlacesResponse response = service.GetResponse(request);
            if (response.Results  != null && response.Results.Count()>0)
                result = response.Results.FirstOrDefault().Name;
            return result;
        }

     
    }
}
