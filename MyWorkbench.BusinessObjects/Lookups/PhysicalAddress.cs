using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Lookups;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [NavigationItem("Lookups")]
    [DefaultProperty("FullAddress")]
    [ImageName("BO_Address")]
    public class PhysicalAddress : BaseObject, IPhysicalAddress, IMapsMarker, IEndlessPaging {
        public PhysicalAddress(Session session)
            : base(session) {
        }

        #region Properties
        private string fLocationName;
        private string fStreet;

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(500)]
        [ToolTip("This can be the unit or complex name and number or a description of the location")]
        public string LocationName {
            get {
                return fLocationName;
            }
            set {
                SetPropertyValue("LocationName", ref fLocationName, value);
            }
        }

        [VisibleInDetailView(false),VisibleInListView(false)]
        public string Title {
            get {
                return fLocationName;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(500)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Street {
            get {
                return fStreet;
            }
            set {
                if (fStreet != null) fStreet = fStreet.Replace(System.Environment.NewLine, " ");
                SetPropertyValue("Street", ref fStreet, value);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(500)]
        public string Suburb { get; set; }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(500)]
        public string City { get; set; }

        private string fPostalCode;

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(10)]
        public string PostalCode {
            get {
                return fPostalCode;
            }
            set {
                SetPropertyValue("PostalCode", ref fPostalCode, value);
            }
        }

        private string fProvince;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(500)]
        public string Province {
            get {
                return fProvince;
            }
            set {
                SetPropertyValue("Province", ref fProvince, value);
            }
        }

        private Country fCountry;

        [VisibleInListView(false), VisibleInDetailView(true)]
        public Country Country {
            get {
                return fCountry;
            }
            set {
                SetPropertyValue("Country", ref fCountry, value);
            }
        }

        private double fLatitude;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public double Latitude {
            get {
                return fLatitude;
            }
            set {
                SetPropertyValue("Latitude", ref fLatitude, value);
            }
        }

        private double fLongitude;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public double Longitude {
            get {
                return fLongitude;
            }
            set {
                SetPropertyValue("Longitude", ref fLongitude, value);
            }
        }

        [VisibleInListView(true), VisibleInDetailView(false)]
        [XafDisplayName("Address")]
        [Persistent("FullAddress")]
        [Size(SizeAttribute.Unlimited)]
        public string FullAddress {
            get {
                string fullAddress = String.Format("{0}{1}{2}{3}{4}",
                    this.LocationName == null ? string.Empty : this.LocationName + ", ",
                    this.Street == null ? string.Empty : this.Street,
                    this.Suburb == null ? string.Empty : ", " + this.Suburb,
                    this.City == null ? string.Empty : ", " + this.City,
                    this.PostalCode == null ? string.Empty : ", " + this.PostalCode
                    );
                return fullAddress.Replace(System.Environment.NewLine, "");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Size(SizeAttribute.Unlimited)]
        public string FormattedFullAddress {
            get {
                return String.Format("{0}{1}{2}{3}{4}{5}{6}",
                    this.LocationName == null ? string.Empty : this.LocationName + Environment.NewLine,
                    this.Street == null ? string.Empty : this.Street,
                    this.Suburb == null ? string.Empty : Environment.NewLine + this.Suburb,
                    this.City == null ? string.Empty : Environment.NewLine + this.City,
                    this.PostalCode == null ? string.Empty : Environment.NewLine + this.PostalCode,
                    this.Province == null ? string.Empty : Environment.NewLine + this.Province,
                    this.Country == null ? string.Empty : Environment.NewLine + this.Country.Name);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Size(SizeAttribute.Unlimited)]
        public string GeocodeFullAddress {
            get {
                string geocodeFullAddress = String.Format("{0}{1}{2}{3}{4}{5}",
                    this.Street == null ? string.Empty : this.Street,
                    this.Suburb == null ? string.Empty : ", " + this.Suburb,
                    this.City == null ? string.Empty : ", " + this.City,
                    this.PostalCode == null ? string.Empty : ", " + this.PostalCode,
                    this.Province == null ? string.Empty : ", " + this.Province,
                    this.Country == null ? string.Empty : ", " + this.Country.Name);

                return geocodeFullAddress.Replace(System.Environment.NewLine, "");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool AddressGeocoded { get; set; }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [XafDisplayName("Geocoded")]
        public Nullable<DateTime> AddressGeocodedDateTime { get; set; }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Appearance("HideSelf", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        public IMapsMarker Self {
            get { return this; }
        }
        #endregion

        #region Collections
        [Association("Vendor_Address")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<Vendor> Vendors {
            get {
                return GetCollection<Vendor>("Vendors");
            }
        }
        #endregion

        #region Methods
        public override void AfterConstruction() {
            this.Province = "Gauteng";
            base.AfterConstruction();
        }
        #endregion
    }
}
