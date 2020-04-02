using Geocoding.Google;
using Ignyt.Framework;
using System.Linq;

namespace Ignyt.Webfunctions
{
    public class GeocodeService : SingletonBase<GeocodeService> {
        public Coordinates Geocode(string ApiKey, string Address) {
            GoogleGeocoder coder = new GoogleGeocoder(ApiKey);

            GoogleAddress address = coder.Geocode(Address).FirstOrDefault();

            if (address != null)
                return new Coordinates() { Latitude = address.Coordinates.Latitude, Longitude = address.Coordinates.Longitude };
            else
                return null;
        }

        public string ReverseGeocode(string ApiKey, double Latitude, double Longitude) {
            GoogleGeocoder coder = new GoogleGeocoder(ApiKey);

            GoogleAddress address = coder.ReverseGeocode(Latitude, Longitude).FirstOrDefault();

            if (address != null)
                return address.FormattedAddress;
            else
                return null;
        }
    }
}
