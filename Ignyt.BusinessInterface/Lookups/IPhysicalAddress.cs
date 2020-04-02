using System;

namespace Ignyt.BusinessInterface.Lookups {
    public interface IPhysicalAddress {
        string Street { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
        string GeocodeFullAddress { get; }
        bool AddressGeocoded { get; set; }
        DateTime? AddressGeocodedDateTime { get; set; }
    }
}
