using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using MyWorkbench.BusinessObjects.Common;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using MyWorkbench.BusinessObjects.Utils;
using MyWorkbench.BusinessObjects.BaseObjects;
using Ignyt.Framework;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace MyWorkbench.BusinessObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [Appearance("SettingsPrefixHideActions", AppearanceItemType = "Action", Criteria = "[SystemGenerated]", TargetItems = "Link;Unlink;Delete;", Enabled = false, Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class SettingsPrefix : BaseObject, IModal
    {
        public SettingsPrefix(Session session)
            : base(session) {
        }

        private Settings fSettings;
        [Association("Settings_SettingsPrefix")]
        [VisibleInListView(false),VisibleInDetailView(false)]
        public Settings Settings {
            get { return fSettings; }
            set {
                SetPropertyValue("Settings", ref fSettings, value);
            }
        }

        private Type fDataType;
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter<WorkflowBase>))]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Source Type")]
        [Indexed("GCRecord", Unique = true)]
        public Type DataType {
            get {
                return fDataType;
            }
            set {
                SetPropertyValue("DataType", ref fDataType, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        public string SourceType {
            get {
                return this.DataType == null ? null : this.DataType.Name;
            }
        }

        private string fPrefix;
        [Size(4)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Prefix {
            get {
                return fPrefix;
            }
            set {
                SetPropertyValue("Prefix", ref fPrefix, value);
            }
        }

        private bool fSystemGenerated;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool SystemGenerated {
            get => fSystemGenerated;
            set => SetPropertyValue(nameof(SystemGenerated), ref fSystemGenerated, value);
        }
    }

    [DefaultClassOptions]
    [NavigationItem(false)]
    [Appearance("SettingsTermsHideActions", AppearanceItemType = "Action", Criteria = "[SystemGenerated]", TargetItems = "Link;Unlink;Delete;", Enabled = false, Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class SettingsTerms : BaseObject, IModal
    {
        public SettingsTerms(Session session)
            : base(session) {
        }

        private Settings fSettings;
        [Association("Settings_SettingsTerms")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Settings Settings {
            get { return fSettings; }
            set {
                SetPropertyValue("Settings", ref fSettings, value);
            }
        }

        private Type fDataType;
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter<WorkflowBase>))]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Source Type")]
        [Indexed("GCRecord", Unique = true)]
        public Type DataType {
            get {
                return fDataType;
            }
            set {
                SetPropertyValue("DataType", ref fDataType, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        public string SourceType {
            get {
                return this.DataType == null ? null : this.DataType.Name;
            }
        }

        private string fTerms;
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "8")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Terms {
            get {
                return fTerms;
            }
            set {
                SetPropertyValue("Terms", ref fTerms, value);
            }
        }

        private bool fSystemGenerated;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool SystemGenerated {
            get => fSystemGenerated;
            set => SetPropertyValue(nameof(SystemGenerated), ref fSystemGenerated, value);
        }
    }

    [DefaultClassOptions]
    [NavigationItem(false)]
    [DefaultProperty("DisplayName")]
    [ImageName("BO_Resume")]
    [Appearance("CultureHideActions", AppearanceItemType = "Action", TargetItems = "Link;Unlink;Delete;Edit;New;Update", Enabled = false, Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class Culture : BaseObject
    {
        public Culture(Session session)
            : base(session)
        {
        }

        private string fName;
        public string Name {
            get { return fName; }
            set {
                SetPropertyValue("Name", ref fName, value);
            }
        }

        private string fDisplayName;
        public string DisplayName {
            get { return fDisplayName; }
            set {
                SetPropertyValue("DisplayName", ref fDisplayName, value);
            }
        }

        private string fTwoLetterISOLanguageName;
        public string TwoLetterISOLanguageName {
            get { return fTwoLetterISOLanguageName; }
            set {
                SetPropertyValue("TwoLetterISOLanguageName", ref fTwoLetterISOLanguageName, value);
            }
        }

        private string fThreeLetterISOLanguageName;
        public string ThreeLetterISOLanguageName {
            get { return fThreeLetterISOLanguageName; }
            set {
                SetPropertyValue("ThreeLetterISOLanguageName", ref fThreeLetterISOLanguageName, value);
            }
        }
    }

    [NavigationItem("Settings")]
    public class Settings : BaseObject, ISingletonBO {
        public Settings(Session session)
            : base(session) {
        }

        #region Properties
        private System.Drawing.Image fLogo;
        [ValueConverter(typeof(ImageValueConverter))]
        [ImageEditor(DetailViewImageEditorFixedHeight = 100)]
        public System.Drawing.Image Logo {
            get => fLogo;
            set => SetPropertyValue(nameof(Logo), ref fLogo, value);
        }

        private string fCompanyName;
        [Size(100)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string CompanyName {
            get => fCompanyName;
            set => SetPropertyValue(nameof(CompanyName), ref fCompanyName, value);
        }

        private string fDescription;
        [Size(100)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private PhysicalAddress fAddress;
        public PhysicalAddress Address {
            get => fAddress;
            set => SetPropertyValue(nameof(Address), ref fAddress, value);
        }

        private string fPOBox;
        [Size(200)]
        public string POBox {
            get => fPOBox;
            set => SetPropertyValue(nameof(POBox), ref fPOBox, value);
        }

        private string fWebsite;
        [Size(100)]
        public string Website {
            get => fWebsite;
            set => SetPropertyValue(nameof(Website), ref fWebsite, value);
        }

        private string fDirectors;
        [Size(100)]
        public string Directors {
            get => fDirectors;
            set => SetPropertyValue(nameof(Directors), ref fDirectors, value);
        }

        private string fContactPerson;
        [Size(100)]
        public string ContactPerson {
            get => fContactPerson;
            set => SetPropertyValue(nameof(ContactPerson), ref fContactPerson, value);
        }

        private string fPhoneNo;
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        public string PhoneNo {
            get => fPhoneNo;
            set => SetPropertyValue(nameof(PhoneNo), ref fPhoneNo, value);
        }

        private string fFaxNo;
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        public string FaxNo {
            get => fFaxNo;
            set => SetPropertyValue(nameof(FaxNo), ref fFaxNo, value);
        }

        private string fEmailAddress;
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Email, CustomMessageTemplate = RegularExpressions.EmailError)]
        public string EmailAddress {
            get => fEmailAddress;
            set => SetPropertyValue(nameof(EmailAddress), ref fEmailAddress, value);
        }

        private string fRegistrationNo;
        [Size(100)]
        [DefaultValue("N/A")]
        public string RegistrationNo {
            get => fRegistrationNo;
            set => SetPropertyValue(nameof(RegistrationNo), ref fRegistrationNo, value);
        }

        private Common.TimeZone fCurrentTimeZone;
        [RuleRequiredField(DefaultContexts.Save)]
        public Common.TimeZone CurrentTimeZone {
            get => fCurrentTimeZone;
            set => SetPropertyValue(nameof(CurrentTimeZone), ref fCurrentTimeZone, value);
        }

        private bool fVatRegistered;
        [DevExpress.Xpo.DisplayName("VAT Registered")]
        [DefaultValue(true)]
        public bool VatRegistered {
            get => fVatRegistered;
            set => SetPropertyValue(nameof(VatRegistered), ref fVatRegistered, value);
        }

        private int fVatPercentage;
        [ToolTip("Set to 0 if you are not VAT registered")]
        [DefaultValue("14")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int VatPercentage {
            get => fVatPercentage;
            set => SetPropertyValue(nameof(VatPercentage), ref fVatPercentage, value);
        }

        private string fVatNo;
        [Size(100)]
        [DefaultValue("N/A")]
        public string VatNo {
            get => fVatNo;
            set => SetPropertyValue(nameof(VatNo), ref fVatNo, value);
        }

        private bool fMessage;
        [DefaultValue(false)]
        public bool Message {
            get => fMessage;
            set => SetPropertyValue(nameof(Message), ref fMessage, value);
        }

        private double fAppointmentLength;
        [DevExpress.Xpo.DisplayName("App Length(Hrs)")]
        [DefaultValue(1.5)]
        public double AppointmentLength {
            get => fAppointmentLength;
            set => SetPropertyValue(nameof(AppointmentLength), ref fAppointmentLength, value);
        }

        private string fApplicationName;
        [Browsable(false)]
        public string ApplicationName {
            get => fApplicationName;
            set => SetPropertyValue(nameof(ApplicationName), ref fApplicationName, value);
        }

        private string fApplicationID;
        [Browsable(false)]
        public string ApplicationID {
            get => fApplicationID;
            set => SetPropertyValue(nameof(ApplicationID), ref fApplicationID, value);
        }

        private Currency fCurrency;
        [RuleRequiredField(DefaultContexts.Save)]
        public Currency Currency {
            get => fCurrency;
            set => SetPropertyValue(nameof(Currency), ref fCurrency, value);
        }

        private string fEmailAccountName;
        public string EmailAccountName {
            get => fEmailAccountName;
            set => SetPropertyValue(nameof(EmailAccountName), ref fEmailAccountName, value);
        }

        private bool fDisableAutomation;
        public bool DisableAutomation {
            get => fDisableAutomation;
            set => SetPropertyValue(nameof(DisableAutomation), ref fDisableAutomation, value);
        }

        private bool fAllowEndlessPaging;
        public bool AllowEndlessPaging {
            get => fAllowEndlessPaging;
            set => SetPropertyValue(nameof(AllowEndlessPaging), ref fAllowEndlessPaging, value);
        }

        private SettingMapProvider fMapProvider;
        public SettingMapProvider MapProvider {
            get => fMapProvider;
            set => SetPropertyValue(nameof(MapProvider), ref fMapProvider, value);
        }

        private string fCellNumber;
        [VisibleInListView(false)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        public string CellNumber {
            get => fCellNumber;
            set => SetPropertyValue(nameof(CellNumber), ref fCellNumber, value);
        }

        private double fDefaultDepositPercentage;
        [VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Default Deposit %")]
        [RuleRange(0, 100)]
        public double DefaultDepositPercentage {
            get => fDefaultDepositPercentage;
            set => SetPropertyValue(nameof(DefaultDepositPercentage), ref fDefaultDepositPercentage, value);
        }

        private double fDefaultMarkupPercentage;
        [DevExpress.Xpo.DisplayName("Default Markup %")]
        [VisibleInListView(false)]
        public double DefaultMarkupPercentage {
            get => fDefaultMarkupPercentage;
            set => SetPropertyValue(nameof(DefaultMarkupPercentage), ref fDefaultMarkupPercentage, value);
        }

        private Vendor fDefaultSaleVendor;
        [VisibleInListView(false)]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        public Vendor DefaultSaleVendor {
            get => fDefaultSaleVendor;
            set => SetPropertyValue(nameof(DefaultSaleVendor), ref fDefaultSaleVendor, value);
        }

        private Culture fCulture;
        [VisibleInListView(false),VisibleInDetailView(true)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public Culture Culture {
            get => fCulture;
            set => SetPropertyValue(nameof(Culture), ref fCulture, value);
        }

        private AccountingPartner fAccountingPartner;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public AccountingPartner AccountingPartner {
            get => fAccountingPartner;
            set => SetPropertyValue(nameof(AccountingPartner), ref fAccountingPartner, value);
        }

        private DayOfWeek fPaymentDayOfWeek;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public DayOfWeek PaymentDayOfWeek {
            get => fPaymentDayOfWeek;
            set => SetPropertyValue(nameof(PaymentDayOfWeek), ref fPaymentDayOfWeek, value);
        }

        private Interval fPaymentInterval;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Interval PaymentInterval {
            get => fPaymentInterval;
            set => SetPropertyValue(nameof(PaymentInterval), ref fPaymentInterval, value);
        }
        #endregion

        #region Collections
        [Association("Settings_SettingsPrefix")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public XPCollection<SettingsPrefix> SystemPrefixes {
            get {
                return GetCollection<SettingsPrefix>("SystemPrefixes");
            }
        }

        [Association("Settings_SettingsTerms")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public XPCollection<SettingsTerms> SystemTerms {
            get {
                return GetCollection<SettingsTerms>("SystemTerms");
            }
        }
        #endregion

        #region Overide Events
        public override void AfterConstruction() {
            base.AfterConstruction();

            this.VatPercentage = 15;
            this.Message = true;
            this.AppointmentLength = 1.5;

            this.PopulateTimeZones();
            this.PopulateCurrency();
        }

        private void PopulateCurrency() {
            this.CreateCurrency("ZAR", "South African Rand");
            this.CreateCurrency("USD", "American Dollars");
            this.CreateCurrency("EUR", "Euro");
            this.CreateCurrency("GBP", "Great British Pound");
            this.CreateCurrency("BWP", "Botswana Pula");
        }

        private void PopulateTimeZones() {
            this.CurrentTimeZone = this.CreateTimeZone("South Africa Standard Time", "(GMT+02:00) Harare, Pretoria");
            this.CreateTimeZone("Morocco Standard Time", "(GMT) Casablanca");
            this.CreateTimeZone("GMT Standard Time", "(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London");
            this.CreateTimeZone("Greenwich Standard Time", "(GMT) Monrovia, Reykjavik");
            this.CreateTimeZone("W. Europe Standard Time", "(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna");
            this.CreateTimeZone("Central Europe Standard Time", "(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague");
            this.CreateTimeZone("Romance Standard Time", "(GMT+01:00) Brussels, Copenhagen, Madrid, Paris");
            this.CreateTimeZone("Central European Standard Time", "(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb");
            this.CreateTimeZone("W. Central Africa Standard Time", "(GMT+01:00) West Central Africa");
            this.CreateTimeZone("Jordan Standard Time", "(GMT+02:00) Amman");
            this.CreateTimeZone("GTB Standard Time", "(GMT+02:00) Athens, Bucharest, Istanbul");
            this.CreateTimeZone("Middle East Standard Time", "(GMT+02:00) Beirut");
            this.CreateTimeZone("Egypt Standard Time", "(GMT+02:00) Cairo");
            this.CreateTimeZone("FLE Standard Time", "(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius");
            this.CreateTimeZone("Israel Standard Time", "(GMT+02:00) Jerusalem");
            this.CreateTimeZone("E. Europe Standard Time", "(GMT+02:00) Minsk");
            this.CreateTimeZone("Namibia Standard Time", "(GMT+02:00) Windhoek");
            this.CreateTimeZone("Arabic Standard Time", "(GMT+03:00) Baghdad");
            this.CreateTimeZone("Arab Standard Time", "(GMT+03:00) Kuwait, Riyadh");
            this.CreateTimeZone("Russian Standard Time", "(GMT+03:00) Moscow, St. Petersburg, Volgograd");
            this.CreateTimeZone("E. Africa Standard Time", "(GMT+03:00) Nairobi");
            this.CreateTimeZone("Georgian Standard Time", "(GMT+03:00) Tbilisi");
            this.CreateTimeZone("Iran Standard Time", "(GMT+03:30) Tehran");
            this.CreateTimeZone("Arabian Standard Time", "(GMT+04:00) Abu Dhabi, Muscat");
            this.CreateTimeZone("Azerbaijan Standard Time", "(GMT+04:00) Baku");
            this.CreateTimeZone("Mauritius Standard Time", "(GMT+04:00) Port Louis");
            this.CreateTimeZone("Caucasus Standard Time", "(GMT+04:00) Yerevan");
            this.CreateTimeZone("Afghanistan Standard Time", "(GMT+04:30) Kabul");
            this.CreateTimeZone("Ekaterinburg Standard Time", "(GMT+05:00) Ekaterinburg");
            this.CreateTimeZone("Pakistan Standard Time", "(GMT+05:00) Islamabad, Karachi");
            this.CreateTimeZone("West Asia Standard Time", "(GMT+05:00) Tashkent");
            this.CreateTimeZone("India Standard Time", "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi");
            this.CreateTimeZone("Sri Lanka Standard Time", "(GMT+05:30) Sri Jayawardenepura");
            this.CreateTimeZone("Nepal Standard Time", "(GMT+05:45) Kathmandu");
            this.CreateTimeZone("N. Central Asia Standard Time", "(GMT+06:00) Almaty, Novosibirsk");
            this.CreateTimeZone("Central Asia Standard Time", "(GMT+06:00) Astana, Dhaka");
            this.CreateTimeZone("Myanmar Standard Time", "(GMT+06:30) Yangon (Rangoon)");
            this.CreateTimeZone("SE Asia Standard Time", "(GMT+07:00) Bangkok, Hanoi, Jakarta");
            this.CreateTimeZone("North Asia Standard Time", "(GMT+07:00) Krasnoyarsk");
            this.CreateTimeZone("China Standard Time", "(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi");
            this.CreateTimeZone("North Asia East Standard Time", "(GMT+08:00) Irkutsk, Ulaan Bataar");
            this.CreateTimeZone("Singapore Standard Time", "(GMT+08:00) Kuala Lumpur, Singapore");
            this.CreateTimeZone("W. Australia Standard Time", "(GMT+08:00) Perth");
            this.CreateTimeZone("Taipei Standard Time", "(GMT+08:00) Taipei");
            this.CreateTimeZone("Tokyo Standard Time", "(GMT+09:00) Osaka, Sapporo, Tokyo");
            this.CreateTimeZone("Korea Standard Time", "(GMT+09:00) Seoul");
            this.CreateTimeZone("Yakutsk Standard Time", "(GMT+09:00) Yakutsk");
            this.CreateTimeZone("Cen. Australia Standard Time", "(GMT+09:30) Adelaide");
            this.CreateTimeZone("AUS Central Standard Time", "(GMT+09:30) Darwin");
            this.CreateTimeZone("E. Australia Standard Time", "(GMT+10:00) Brisbane");
            this.CreateTimeZone("AUS Eastern Standard Time", "(GMT+10:00) Canberra, Melbourne, Sydney");
            this.CreateTimeZone("West Pacific Standard Time", "(GMT+10:00) Guam, Port Moresby");
            this.CreateTimeZone("Tasmania Standard Time", "(GMT+10:00) Hobart");
            this.CreateTimeZone("Vladivostok Standard Time", "(GMT+10:00) Vladivostok");
            this.CreateTimeZone("Central Pacific Standard Time", "(GMT+11:00) Magadan, Solomon Is., New Caledonia");
            this.CreateTimeZone("New Zealand Standard Time", "(GMT+12:00) Auckland, Wellington");
            this.CreateTimeZone("Fiji Standard Time", "(GMT+12:00) Fiji, Kamchatka, Marshall Is.");
            this.CreateTimeZone("Tonga Standard Time", "(GMT+13:00) Nuku'alofa");
            this.CreateTimeZone("Azores Standard Time", "(GMT-01:00) Azores");
            this.CreateTimeZone("Cape Verde Standard Time", "(GMT-01:00) Cape Verde Is.");
            this.CreateTimeZone("Mid-Atlantic Standard Time", "(GMT-02:00) Mid-Atlantic");
            this.CreateTimeZone("E. South America Standard Time", "(GMT-03:00) Brasilia");
            this.CreateTimeZone("Argentina Standard Time", "(GMT-03:00) Buenos Aires");
            this.CreateTimeZone("SA Eastern Standard Time", "(GMT-03:00) Georgetown");
            this.CreateTimeZone("Greenland Standard Time", "(GMT-03:00) Greenland");
            this.CreateTimeZone("Montevideo Standard Time", "(GMT-03:00) Montevideo");
            this.CreateTimeZone("Newfoundland Standard Time", "(GMT-03:30) Newfoundland");
            this.CreateTimeZone("Atlantic Standard Time", "(GMT-04:00) Atlantic Time (Canada)");
            this.CreateTimeZone("SA Western Standard Time", "(GMT-04:00) La Paz");
            this.CreateTimeZone("Central Brazilian Standard Time", "(GMT-04:00) Manaus");
            this.CreateTimeZone("Pacific SA Standard Time", "(GMT-04:00) Santiago");
            this.CreateTimeZone("Venezuela Standard Time", "(GMT-04:30) Caracas");
            this.CreateTimeZone("SA Pacific Standard Time", "(GMT-05:00) Bogota, Lima, Quito, Rio Branco");
            this.CreateTimeZone("Eastern Standard Time", "(GMT-05:00) Eastern Time (US & Canada)");
            this.CreateTimeZone("US Eastern Standard Time", "(GMT-05:00) Indiana (East)");
            this.CreateTimeZone("Central America Standard Time", "(GMT-06:00) Central America");
            this.CreateTimeZone("Central Standard Time", "(GMT-06:00) Central Time (US & Canada)");
            this.CreateTimeZone("Central Standard Time (Mexico)", "(GMT-06:00) Guadalajara, Mexico City, Monterrey");
            this.CreateTimeZone("Canada Central Standard Time", "(GMT-06:00) Saskatchewan");
            this.CreateTimeZone("US Mountain Standard Time", "(GMT-07:00) Arizona");
            this.CreateTimeZone("Mountain Standard Time (Mexico)", "(GMT-07:00) Chihuahua, La Paz, Mazatlan");
            this.CreateTimeZone("Mountain Standard Time", "(GMT-07:00) Mountain Time (US & Canada)");
            this.CreateTimeZone("Pacific Standard Time", "(GMT-08:00) Pacific Time (US & Canada)");
            this.CreateTimeZone("Pacific Standard Time (Mexico)", "(GMT-08:00) Tijuana, Baja California");
            this.CreateTimeZone("Alaskan Standard Time", "(GMT-09:00) Alaska");
            this.CreateTimeZone("Hawaiian Standard Time", "(GMT-10:00) Hawaii");
            this.CreateTimeZone("Samoa Standard Time", "(GMT-11:00) Midway Island, Samoa");
            this.CreateTimeZone("Dateline Standard Time", "(GMT-12:00) International Date Line West");
        }

        private Common.TimeZone CreateTimeZone(string Value, string Description) {
            Common.TimeZone _timeZone = this.Session.FindObject<Common.TimeZone>(new BinaryOperator("Value", Value));

            if (_timeZone == null) {
                _timeZone = new Common.TimeZone(this.Session) {
                    Value = Value,
                    Description = Description
                };
                _timeZone.Save();
            }

            return _timeZone;
        }

        private Currency CreateCurrency(string Value, string Description) {
            Currency _currency = this.Session.FindObject<Currency>(new BinaryOperator("Value", Value));

            if (_currency == null) {
                _currency = new Currency(this.Session) {
                    Value = Value,
                    Description = Description
                };
                _currency.Save();
            }

            return _currency;
        }

        protected override void OnDeleting() {
            MessageProvider.RegisterMessage(new MessageInformation(MessageTypes.Warning, "Settings cannot be deleted."));
        }
        #endregion
    }
}
