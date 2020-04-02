using DevExpress.Data.Filtering;
using Ignyt.Framework;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.BusinessObjects.Helpers {
    public class VendorHelper : SingletonBase<VendorHelper> {
        public void CreateContactInfo(Vendor Vendor) {
            if (Vendor.VendorContacts.Count == 0) {
                if (Vendor.FirstName != null && Vendor.LastName != null && Vendor.Email != null) {
                    VendorContact contact = Vendor.Session.FindObject<VendorContact>(CriteriaOperator.Parse("Email == ?", Vendor.Email));

                    if (contact == null)
                        contact = new VendorContact(Vendor.Session) { FirstName = Vendor.FirstName, LastName = Vendor.LastName, Phone = Vendor.Phone, CellNo = Vendor.CellNo, Email = Vendor.Email };

                    Vendor.VendorContacts.Add(contact);
                }
            }
        }

        public void CreateLocationInfo(Vendor Vendor) {
            if (Vendor.Locations.Count == 0) {
                if (Vendor.PostalAddress != null) {
                    VendorContact contact = Vendor.Session.FindObject<VendorContact>(CriteriaOperator.Parse("Email == ?", Vendor.Email));

                    Location location = Vendor.Session.FindObject<Location>(contact != null ? CriteriaOperator.Parse("PhysicalAddress == ? and VendorContact == ?", Vendor.PostalAddress.Oid, contact.Oid) : CriteriaOperator.Parse("PhysicalAddress == ?", Vendor.PostalAddress.Oid));

                    if (location == null)
                        location = new Location(Vendor.Session) { PhysicalAddress = Vendor.PostalAddress, VendorContact = contact };

                    Vendor.Locations.Add(location);
                }
            }
        }
    }
}
