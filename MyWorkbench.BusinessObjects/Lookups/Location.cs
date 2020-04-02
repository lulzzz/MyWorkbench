using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Filtering;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Inventory;
using Ignyt.BusinessInterface;
using MyWorkbench.BaseObjects.Constants;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects.Lookups
{
    [DefaultClassOptions]
    [DefaultProperty("FullAddress")]
    [NavigationItem("Lookups")]
    [ImageName("BO_Address")]
    public class Location : BaseObject, IEndlessPaging
    {
        public Location(Session session)
            : base(session)
        {
        }

        #region Properties
        private VendorContact fVendorContact;
        [Association("VendorContact_Location")]
        [DataSourceProperty("Vendor.VendorContacts")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public VendorContact VendorContact {
            get {
                return fVendorContact;
            }
            set {
                SetPropertyValue("VendorContact", ref fVendorContact, value);
            }
        }

        private PhysicalAddress fPhysicalAddress;
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DevExpress.Xpo.DisplayName("Location/Address")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public PhysicalAddress PhysicalAddress {
            get {
                return fPhysicalAddress;
            }
            set {
                SetPropertyValue("PhysicalAddress", ref fPhysicalAddress, value);
            }
        }

        private Vendor fVendor;
        [Association("Vendor_Location")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [Persistent("FullAddress")]
        [SearchMemberOptions(SearchMemberMode.Include)]
        public string FullAddress {
            get {
                if (PhysicalAddress != null && this.VendorContact != null)
                    return String.Format("{0} - {1}", PhysicalAddress.FullAddress, VendorContact.FullName);
                else if (PhysicalAddress != null)
                    return PhysicalAddress.FullAddress;
                else
                {
                    return "N/A";
                }
            }
        }

        private Party fParty;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private DateTime fCreated;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime Created {
            get => fCreated;
            set => SetPropertyValue(nameof(Created), ref fCreated, value);
        }
        #endregion

        #region Collections
        [Association("Location_WorkflowBase")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }

        [DevExpress.Xpo.DisplayName("Equipment")]
        [Association("Location_Equipment")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<Equipment> Equipments {
            get {
                return GetCollection<Equipment>("Equipments");
            }
        }
        #endregion

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.Created = Constants.DateTimeTimeZone(this.Session);
            }
        }

        protected override void OnSaving()
        {
            VendorHelper.Instance.CreateContactInfo(this.Vendor);

            base.OnSaving();
        }
    }
}
