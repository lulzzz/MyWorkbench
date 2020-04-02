using Ignyt.Framework;
using Ignyt.Webfunctions;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Lookups;
using DevExpress.Data.Filtering;
using MyWorkbench.BaseObjects.Constants;

namespace MyWorkbench.BusinessObjects.Helpers {
    public static class AddressBaseHelper {
        public static void ReProcessGeocode(PhysicalAddress PhysicalAddress)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(PhysicalAddress.Session.DataLayer))
            {
                PhysicalAddress physicalAddress = unitOfWork.FindObject<PhysicalAddress>(CriteriaOperator.Parse("Oid == ?", PhysicalAddress.Oid));

                physicalAddress.AddressGeocoded = false;
                physicalAddress.AddressGeocodedDateTime = null;

                unitOfWork.CommitChanges();
            }
        }

        public static void UpdateGeocodeProcessed(PhysicalAddress PhysicalAddress, Coordinates Coordinates)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(PhysicalAddress.Session.DataLayer))
            {
                PhysicalAddress physicalAddress = unitOfWork.FindObject<PhysicalAddress>(CriteriaOperator.Parse("Oid == ?", PhysicalAddress.Oid));

                physicalAddress.AddressGeocodedDateTime = Constants.DateTimeTimeZone(unitOfWork);
                physicalAddress.Latitude = Coordinates == null ? 0 : Coordinates.Latitude;
                physicalAddress.Longitude = Coordinates == null ? 0 : Coordinates.Longitude;
                physicalAddress.AddressGeocoded = true;

                unitOfWork.CommitChanges();
            }
        }
    }
}
