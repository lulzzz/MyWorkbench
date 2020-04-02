using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.BusinessObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    public class FileAttachment : AttachmentBase {
        public FileAttachment(Session session)
            : base(session) {
        }

        #region Properties
        private Vendor fVendor;
        [Association("Vendor_FileAttachment")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);
            }
        }

        private VendorContact fVendorContact;
        [Association("VendorContact_FileAttachment")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public VendorContact VendorContact {
            get {
                return fVendorContact;
            }
            set {
                SetPropertyValue("VendorContact", ref fVendorContact, value);
            }
        }

        private Team fTeam;
        [Association("Team_FileAttachment")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Team Team {
            get {
                return fTeam;
            }
            set {
                SetPropertyValue("Team", ref fTeam, value);
            }
        }

        private Employee fEmployee;
        [Association("Employee_FileAttachment")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Employee Employee {
            get {
                return fEmployee;
            }
            set {
                SetPropertyValue("Employee", ref fEmployee, value);
            }
        }
        #endregion
    }
}
