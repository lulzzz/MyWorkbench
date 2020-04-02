using Geocoding.Google;
using Ignyt.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Ignyt.Framework.Services {
    public class GeocodeService : SingletonBase<GeocodeService> {
        public Coordinates Geocode(string ApiKey, string Address) {
            GoogleGeocoder coder = new GoogleGeocoder(ApiKey);

            IEnumerable<GoogleAddress> address = coder.Geocode(Address);

            //if (address != null)
            //    return new Coordinates() { Latitude = address.Coordinates.Latitude, Longitude = address.Coordinates.Longitude };
            //else
            //    return null;

            return null;
        }

        public string ReverseGeocode(string ApiKey, double Latitude, double Longitude) {
            GoogleGeocoder coder = new GoogleGeocoder(ApiKey);

            GoogleAddress address = coder.ReverseGeocode(Latitude, Longitude).FirstOrDefault();

            coder = null;

            if (address != null)
                return address.FormattedAddress;
            else
                return null;
        }
    }
}
