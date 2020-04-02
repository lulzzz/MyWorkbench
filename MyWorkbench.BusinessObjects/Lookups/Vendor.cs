using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Communication.Enum;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Communication.Common;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Lookups
{
    [DefaultClassOptions]
    [NavigationItem("People")]
    [DefaultProperty("FullName")]
    [ImageName("BO_Customer")]
    public class Vendor : BaseObject, IEndlessPaging, IEmailAddress, IEmailPopup,
        IMessagePopup, IVendor, IRecipientType, ICellNumber, IAccountingPartner
    {
        public Vendor(Session session)
            : base(session)
        {
        }

        #region Properties
        private System.Drawing.Image fImage;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 160, DetailViewImageEditorFixedHeight = 160, ListViewImageEditorCustomHeight = 48)]
        public System.Drawing.Image Image {
            get {
                return fImage;
            }
            set {
                SetPropertyValue("Image", ref fImage, value);
            }
        }

        private VendorType fAccountType;
        [RuleRequiredField("Account Type is required", DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public VendorType AccountType {
            get => fAccountType;
            set => SetPropertyValue(nameof(AccountType), ref fAccountType, value);
        }

        private string fFirstName;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(100)]
        [RuleRequiredField("First Name is required", DefaultContexts.Save, "First Name is required if Company is empty", TargetCriteria = "Company = null")]
        public string FirstName {
            get => fFirstName;
            set => SetPropertyValue(nameof(FirstName), ref fFirstName, value);
        }

        private string fLastName;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(100)]
        [RuleRequiredField("Last Name is required", DefaultContexts.Save, "Last Name is required if Company is empty", TargetCriteria = "Company = null")]
        public string LastName {
            get => fLastName;
            set => SetPropertyValue(nameof(LastName), ref fLastName, value);
        }

        private string fCompany;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(100)]
        [RuleRequiredField("Company Name is required", DefaultContexts.Save, "Company is required if First Name or Last Name is empty", TargetCriteria = "LastName = null or FirstName = null")]
        public string Company {
            get => fCompany;
            set => SetPropertyValue(nameof(Company), ref fCompany, value);
        }

        private string fPhone;
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Size(100)]
        public string Phone {
            get => fPhone;
            set => SetPropertyValue(nameof(Phone), ref fPhone, value);
        }

        private string fCell;
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(100)]
        public string CellNo {
            get => fCell;
            set => SetPropertyValue(nameof(CellNo), ref fCell, value);
        }

        private string fAltPhone;
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(100)]
        public string AltPhone {
            get => fAltPhone;
            set => SetPropertyValue(nameof(AltPhone), ref fAltPhone, value);
        }

        private string fEmail;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Email, CustomMessageTemplate = RegularExpressions.EmailError)]
        [RuleRequiredField("Email is required", DefaultContexts.Save, "Email is required if First and Last Name are specified", TargetCriteria = "Company = null")]
        public string Email {
            get => fEmail;
            set => SetPropertyValue(nameof(Email), ref fEmail, value);
        }

        private string fIDNumber;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(13)]
        [DevExpress.Xpo.DisplayName("ID No")]
        public string IDNumber {
            get => fIDNumber;
            set => SetPropertyValue(nameof(IDNumber), ref fIDNumber, value);
        }

        private PhysicalAddress fPostalAddress;
        [Association("Vendor_Address")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public PhysicalAddress PostalAddress {
            get {
                return fPostalAddress;
            }
            set {
                SetPropertyValue("PostalAddress", ref fPostalAddress, value);
            }
        }

        private string fVATNo;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(20)]
        [DevExpress.Xpo.DisplayName("VAT No")]
        public string VATNo {
            get => fVATNo;
            set => SetPropertyValue(nameof(VATNo), ref fVATNo, value);
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(true)]
        [PersistentAlias("Iif(Company is null || Company = '' || Company = ' ', Trim(Concat(FirstName, ' ',LastName)), Company)")]
        public string FullName {
            get {
                object tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        [Size(-1)]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string Description {
            get {
                return String.Format("{0} ({1})", FullName, this.AccountType.ToString());
            }
        }

        private Source fSource;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Association("Source_Vendor")]
        public Source Source {
            get {
                return fSource;
            }
            set {
                SetPropertyValue("Source", ref fSource, value);
            }
        }

        private VendorStatus fStatus;
        [RuleRequiredField("Status is required", DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public VendorStatus Status {
            get {
                return fStatus;
            }
            set {
                SetPropertyValue("Status", ref fStatus, value);
            }
        }

        private string fNotes;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(SizeAttribute.Unlimited)]
        public string Notes {
            get {
                return fNotes;
            }
            set {
                SetPropertyValue("Notes", ref fNotes, value);
            }
        }

        private CommonEnum fTitle;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [DevExpress.Xpo.DisplayName("Title")]
        public CommonEnum PersonTitle {
            get {
                return fTitle;
            }
            set {
                SetPropertyValue("Title", ref fTitle, value);
            }
        }

        private string fExternalAccountsCode;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Account Code")]
        public string ExternalAccountsCode {
            get {
                return fExternalAccountsCode;
            }
            set {
                SetPropertyValue("ExternalAccountsCode", ref fExternalAccountsCode, value);
            }
        }

        private string fProfileNumber;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string ProfileNumber {
            get {
                return fProfileNumber;
            }
            set {
                SetPropertyValue("ProfileNumber", ref fProfileNumber, value);
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

        private WorkflowResource fResource;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public WorkflowResource WorkflowResource {
            get => fResource;
            set => SetPropertyValue(nameof(Resource), ref fResource, value);
        }

        [Persistent("Color")]
        private Int32 FColor;
        [NonPersistent]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public System.Drawing.Color Color {
            get { return System.Drawing.Color.FromArgb(FColor); }
            set {
                FColor = value.ToArgb();
                OnChanged("Color");
            }
        }
        #endregion

        private string fAccountingPartnerIdentifier;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AccountingPartnerIdentifier {
            get => fAccountingPartnerIdentifier;
            set => SetPropertyValue(nameof(AccountingPartnerIdentifier), ref fAccountingPartnerIdentifier, value);
        }

        #region Aging
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Appearance("BalanceEnabled", Enabled = false, Context = "DetailView")]
        public double Balance {
            get {
                if (IsLoading) return 0;

                if(this.AccountType == VendorType.Client)
                    return Math.Round(this.Transactions.Sum(g => g.Debit) - this.Transactions.Sum(g => g.Credit), 2);
                else
                    return Math.Round(this.Transactions.Sum(g => g.Credit) - this.Transactions.Sum(g => g.Debit), 2);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Appearance("CurrentEnabled", Enabled = false, Context = "DetailView")]
        public double Current {
            get {
                if (IsLoading) return 0;

                if (this.AccountType == VendorType.Client)
                    return Math.Round(this.Transactions.Where(g => g.Current).Sum(g => g.Debit) - this.Transactions.Where(g => g.Current).Sum(g => g.Credit), 2);
                else
                    return Math.Round(this.Transactions.Where(g => g.Current).Sum(g => g.Credit) - this.Transactions.Where(g => g.Current).Sum(g => g.Debit), 2);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Appearance("ThirtyDaysEnabled", Enabled = false, Context = "DetailView")]
        public double ThirtyDays {
            get {
                if (IsLoading) return 0;

                if (this.AccountType == VendorType.Client)
                    return Math.Round(this.Transactions.Where(g => g.ThirtyDays).Sum(g => g.Debit) - this.Transactions.Where(g => g.ThirtyDays).Sum(g => g.Credit), 2);
                else
                    return Math.Round(this.Transactions.Where(g => g.ThirtyDays).Sum(g => g.Credit) - this.Transactions.Where(g => g.ThirtyDays).Sum(g => g.Debit), 2);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Appearance("SixtyDaysEnabled", Enabled = false, Context = "DetailView")]
        public double SixtyDays {
            get {
                if (IsLoading) return 0;

                if (this.AccountType == VendorType.Client)
                    return Math.Round(this.Transactions.Where(g => g.SixtyDays).Sum(g => g.Debit) - this.Transactions.Where(g => g.SixtyDays).Sum(g => g.Credit), 2);
                else
                    return Math.Round(this.Transactions.Where(g => g.SixtyDays).Sum(g => g.Credit) - this.Transactions.Where(g => g.SixtyDays).Sum(g => g.Debit), 2);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Appearance("NinetyDaysEnabled", Enabled = false, Context = "DetailView")]
        public double NinetyDays {
            get {
                if (IsLoading) return 0;

                if (this.AccountType == VendorType.Client)
                    return Math.Round(this.Transactions.Where(g => g.NinetyDays).Sum(g => g.Debit) - this.Transactions.Where(g => g.NinetyDays).Sum(g => g.Credit), 2);
                else
                    return Math.Round(this.Transactions.Where(g => g.NinetyDays).Sum(g => g.Credit) - this.Transactions.Where(g => g.NinetyDays).Sum(g => g.Debit), 2);
            }
        }
        #endregion

        #region Collections
        [Association("Vendor_Transaction")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<Transaction> Transactions {
            get {
                return GetCollection<Transaction>("Transactions");
            }
        }

        [Association("Vendor_VendorContact")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<VendorContact> VendorContacts {
            get {
                return GetCollection<VendorContact>("VendorContacts");
            }
        }

        [Association("Vendor_Location"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<Location> Locations {
            get {
                return GetCollection<Location>("Locations");
            }
        }

        [Association("Vendor_WorkflowBase")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }

        [Association("Vendor_FileAttachment")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<FileAttachment> Attachments {
            get {
                return GetCollection<FileAttachment>("Attachments");
            }
        }

        [Association("Vendor_Equipment")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<Equipment> Equipment {
            get {
                return GetCollection<Equipment>("Equipment");
            }
        }

        [Association("Vendor_RequestForQuote")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [EditorAlias("TokenCollectionEditor")]
        public XPCollection<RequestForQuote> RequestForQuotes {
            get {
                return GetCollection<RequestForQuote>("RequestForQuotes");
            }
        }
        #endregion

        #region Interfaces
        private Guid fApplicationID;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Guid ApplicationID {
            get {
                if (fApplicationID == Guid.Empty)
                    fApplicationID = Constants.ApplicationID(this.Session);
                return fApplicationID;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public Party SendingUser {
            get {
                if (SecuritySystem.CurrentUserId.ToString() == string.Empty)
                    return this.Party;
                else
                    return Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Title {
            get {
                return string.Format("{0}", this.GetType().Name);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IEmailAddress> EmailAddresses {
            get {
                List<IEmailAddress> emailAddresses = new List<IEmailAddress>();

                foreach (var vendorContact in this.VendorContacts)
                    emailAddresses.AddRangeUnique(vendorContact.EmailAddresses);

                emailAddresses.AddRangeUnique(new List<IEmailAddress> {
                    new EmailAddress(this.Session) { Email = this.Email, FullName = this.FullName, RecipientRole = this.RecipientRole } }
                );

                return emailAddresses;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string RecipientDescription {
            get {
                return string.Format("{0}", this.GetType().Name);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<ICellNumber> CellNumbers {
            get {
                if (string.IsNullOrEmpty(this.CellNo))
                    return null;

                return new List<ICellNumber> {
                    new CellNumber(this.Session) { CellNo = this.CellNo, FullName = this.FullName, RecipientRole = this.RecipientRole }
                };
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IFileAttachment> EmailAttachments {
            get {
                return this.Attachments.ToList();
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public KeyValuePair<string, string> KeyValuePair {
            get {
                return new KeyValuePair<string, string>(this.Email, this.FullName);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string ReportDisplayName { get { return "Print Statement"; } }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string ReportFileName { get { return this.FullName; } }

        private byte[] ftext = null;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonPersistent]
        public byte[] Text { get { return ftext; } set { ftext = value; } }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public RecipientRoles RecipientRole {
            get {
                return RecipientRoles.Vendor;
            }
        }
        #endregion

        #region Events
        protected override void OnSaving()
        {
            if (this.WorkflowResource == null)
                this.WorkflowResource = new WorkflowResource(this.Session) { Caption = this.FullName, Color = this.Color,
                    ResourceType = this.GetType(),
                    ObjectOid = this.Oid, Image = this.Image
                };
            else
            {
                this.WorkflowResource.Caption = this.FullName;
                this.WorkflowResource.Color = this.Color;
                this.WorkflowResource.ResourceType = this.GetType();
                this.WorkflowResource.ObjectOid = this.Oid;
                this.WorkflowResource.Image = this.Image;
            }

            VendorHelper.Instance.CreateContactInfo(this);
            VendorHelper.Instance.CreateLocationInfo(this);

            base.OnSaving();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.Created = Constants.DateTimeTimeZone(this.Session);
                this.Status = VendorStatus.Active;
            }
        }
        #endregion
    }
}
