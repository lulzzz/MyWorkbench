using Ignyt.Webfunctions;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkBench.Service.Api { 
    public class GeoCode : ApiBase
    {
        private string GoogleMapsApi { get; set; }

        public GeoCode(string ConnectionString, string GoogleMapsApi) : base(ConnectionString) {
            this.GoogleMapsApi = GoogleMapsApi;
        }

        protected override void ProcessClient()
        {
            try
            {
                base.ProcessClient();

                this.ProcessGeocodes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessGeocodes()
        {
            this.ProcessGeocode();
            this.DataSourceHelper.Dispose();
        }

        private void ProcessGeocode()
        {
            try
            {
                List<PhysicalAddress> physicalAddress = this.DataSourceHelper.Select<PhysicalAddress>(typeof(PhysicalAddress), "AddressGeocodedDateTime Is Null").OfType<PhysicalAddress>().ToList();

                if (physicalAddress.Count >= 1)
                    physicalAddress.ForEach(ProcessGeocode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessGeocode(PhysicalAddress PhysicalAddress)
        {
            Coordinates coordinates = null;

            try
            {
                coordinates = GeocodeService.Instance.Geocode(this.GoogleMapsApi, PhysicalAddress.GeocodeFullAddress);

                AddressBaseHelper.UpdateGeocodeProcessed(PhysicalAddress, coordinates);
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
            finally {
                AddressBaseHelper.UpdateGeocodeProcessed(PhysicalAddress, coordinates);
            }
        }
    }
}
