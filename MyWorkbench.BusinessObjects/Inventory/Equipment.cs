using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Inventory {
    [DefaultClassOptions]
    [NavigationItem("Inventory")]
    [ImageName("Action_Debug_Stop")]
    [DefaultProperty("FullDescription")]
    public class Equipment : BaseObject, IEndlessPaging {
        public Equipment(Session session)
            : base(session) {
        }

        #region Properties
        private Vendor fVendor;
        [Association("Vendor_Equipment")]
        [ImmediatePostData(true)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "1")]
        [XafDisplayName("Client")]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);

                if (!IsLoading && !IsSaving)
                    if (this.Vendor != null)
                        this.Location = this.Vendor.Locations.Count > 0 ? this.Vendor.Locations.First() : null;
            }
        }

        private Location fLocation;
        [Association("Location_Equipment")]
        [DataSourceProperty("Vendor.Locations")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "2")]
        public Location Location {
            get {
                return fLocation;
            }
            set {
                SetPropertyValue("Location", ref fLocation, value);
            }
        }

        private string fSerialNumber;
        [NonCloneable]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "3")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SerialNumber {
            get {
                return fSerialNumber;
            }
            set {
                SetPropertyValue("SerialNumber", ref fSerialNumber, value);
            }
        }

        private Make fMake;
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData(true)]
        [Association("Make_Equipment")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Make Make {
            get {
                return fMake;
            }
            set {
                SetPropertyValue("Make", ref fMake, value);
            }
        }

        private Model fModel;
        [DataSourceProperty("Make.Models")]
        [Association("Model_Equipment")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Model Model {
            get {
                return fModel;
            }
            set {
                SetPropertyValue("Model", ref fModel, value);
            }
        }

        private EquipmentType fEquipmentType;
        [Association("EquipmentType_Equipment")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "4")]
        [RuleRequiredField(DefaultContexts.Save)]
        public EquipmentType EquipmentType {
            get {
                return fEquipmentType;
            }
            set {
                SetPropertyValue("EquipmentType", ref fEquipmentType, value);
            }
        }

        private int fWarranty;
        [VisibleInDetailView(true),VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Warranty (Months)")]
        [ModelDefault("Index", "5")]
        public int Warranty {
            get {
                return fWarranty;
            }
            set {
                SetPropertyValue("Warranty", ref fWarranty, value);
            }
        }

        private DateTime fDateManufactured;
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public DateTime DateManufactured {
            get {
                return fDateManufactured;
            }
            set {
                SetPropertyValue("DateManufactured", ref fDateManufactured, value);
            }
        }

        private System.Drawing.Image fImage;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 160, DetailViewImageEditorFixedHeight = 160)]
        [ModelDefault("Index", "6")]
        public System.Drawing.Image Image {
            get {
                return fImage;
            }
            set {
                SetPropertyValue("Image", ref fImage, value);
            }
        }

        private DateTime fDateTime;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        public DateTime DatePurchased {
            get {
                return fDateTime;
            }
            set {
                SetPropertyValue("DatePurchased", ref fDateTime, value);
            }
        }

        private string fAccessories;
        [Size(500)]
        [VisibleInListView(false),VisibleInDetailView(true)]
        public string Accessories {
            get {
                return fAccessories;
            }
            set {
                SetPropertyValue("Accessories", ref fAccessories, value);
            }
        }

        private double fRunningHoursPerDay;
        [VisibleInListView(false),VisibleInDetailView(true)]
        public double RunningHoursPerDay {
            get {
                return fRunningHoursPerDay;
            }
            set {
                SetPropertyValue("RunningHoursPerDay", ref fRunningHoursPerDay, value);
            }
        }

        private double fHoursBetweenServices;
        [VisibleInListView(false),VisibleInDetailView(true)]
        public double HoursBetweenServices {
            get {
                return fHoursBetweenServices;
            }
            set {
                SetPropertyValue("HoursBetweenServices", ref fHoursBetweenServices, value);
            }
        }

        private int fRunningDaysPerWeek;
        [VisibleInListView(false),VisibleInDetailView(true)]
        public int RunningDaysPerWeek {
            get {
                return fRunningDaysPerWeek;
            }
            set {
                SetPropertyValue("RunningDaysPerWeek", ref fRunningDaysPerWeek, value);
            }
        }

        private DateTime fLastServiceDate;
        [VisibleInListView(false),VisibleInDetailView(true)]
        public DateTime LastServiceDate {
            get {
                return fLastServiceDate;
            }
            set {
                SetPropertyValue("LastServiceDate", ref fLastServiceDate, value);
            }
        }

        [PersistentAlias("Iif(LastServiceDate != null && HoursBetweenServices > 0 && RunningHoursPerDay > 0 && RunningDaysPerWeek > 0, AddDays(LastServiceDate, HoursBetweenServices/RunningHoursPerDay*7/RunningDaysPerWeek), #01/01/0001#)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public DateTime NextServiceDate {
            get {
                object tempObject = EvaluateAlias("NextServiceDate");
                return (DateTime)tempObject;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Concat([Make.Description],' - ', [EquipmentType.Description], ' - ', [SerialNumber])")]
        public string FullDescription {
            get {
                return (string)EvaluateAlias("FullDescription");
            }
        }
        #endregion

        #region Collections
        [Association("Equipment_WorkFlowEquipment")]
        [NonCloneable]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [XafDisplayName("Equipment")]
        public XPCollection<WorkFlowEquipment> WorkFlowEquipments {
            get {
                return GetCollection<WorkFlowEquipment>("WorkFlowEquipments");
            }
        }
        #endregion
    }
}
