using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using MyWorkbench.BusinessObjects.BaseObjects;
using Ignyt.BusinessInterface;
using DevExpress.ExpressApp;
using MyWorkbench.BaseObjects.Constants;
using System.Collections.Generic;
using System.Linq;
using Ignyt.Framework;
using MyWorkbench.BusinessObjects.Communication.Common;
using MyWorkbench.BusinessObjects.Communication;
using Ignyt.BusinessInterface.Attributes;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem("People")]
    [ImageName("BO_Customer")]
    [RuleCombinationOfPropertiesIsUnique("EmailRule", DefaultContexts.Save, "Email")]
    public class VendorContact : BaseObject, IEmailAddress, ICellNumber, IModal
    {
        public VendorContact(Session session)
            : base(session) {
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

        private Vendor fVendor;
        [Association("Vendor_VendorContact")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);
            }
        }

        private string fFirstName;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Size(100)]
        [RuleRequiredField("Contract First Name is required", DefaultContexts.Save)]
        public string FirstName {
            get {
                return fFirstName;
            }
            set {
                SetPropertyValue("FirstName", ref fFirstName, value);
            }
        }

        private string fLastName;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Size(100)]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleRequiredField("Contact Last Name is required", DefaultContexts.Save)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Ascending)]
        public string LastName {
            get {
                return fLastName;
            }
            set {
                SetPropertyValue("LastName", ref fLastName, value);
            }
        }

        private string fEmail;
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Email, CustomMessageTemplate = RegularExpressions.EmailError)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [Indexed("GCRecord", Unique = true)]
        public string Email {
            get {
                return fEmail;
            }
            set {
                SetPropertyValue("Email", ref fEmail, value);
            }
        }

        private string fCellNo;
        [Size(100)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string CellNo {
            get {
                return fCellNo;
            }
            set {
                SetPropertyValue("CellNo", ref fCellNo, value);
            }
        }

        private string fPhone;
        [Size(100)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        public string Phone {
            get {
                return fPhone;
            }
            set {
                SetPropertyValue("Phone", ref fPhone, value);
            }
        }

        private ContactType fContactType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public ContactType ContactType {
            get {
                return fContactType;
            }
            set {
                SetPropertyValue("ContactType", ref fContactType, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [NonCloneable]
        public string Description {
            get {
                return string.Format("{0}{1}{2}",this.FullName, " - ", this.Email == null ? this.CellNo : this.Email);
            }

        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string FullName {
            get {
                return String.Format("{0} {1}", FirstName, LastName);
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
        [Association("VendorContact_WorkflowBase")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }

        [Association("VendorContact_Location")]
        public XPCollection<Location> Locations {
            get {
                return GetCollection<Location>("Locations");
            }
        }

        [Association("VendorContact_FileAttachment")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<FileAttachment> Attachments {
            get {
                return GetCollection<FileAttachment>("Attachments");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("Email_VendorContact")]
        public XPCollection<Email> ToEmails {
            get {
                return GetCollection<Email>("ToEmails");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("CCEmail_VendorContact")]
        public XPCollection<Email> CCEmails {
            get {
                return GetCollection<Email>("CCEmails");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("BCCEmail_VendorContact")]
        public XPCollection<Email> BCCEmails {
            get {
                return GetCollection<Email>("BCCEmails");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("Message_VendorContact")]
        public XPCollection<Message> Messages {
            get {
                return GetCollection<Message>("Messages");
            }
        }
        #endregion

        #region Interfaces
        private IEnumerable<IEmailAddress> fEmailAddresses;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IEmailAddress> EmailAddresses {
            get {
                if (fEmailAddresses == null)
                    fEmailAddresses = new List<IEmailAddress>() {
                    new EmailAddress(this.Session) { Email = this.Email, FullName = this.FullName, RecipientRole = this.RecipientRole }
                };
                return fEmailAddresses;
            }
        }

        private IEnumerable<ICellNumber> fCellNumbers;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<ICellNumber> CellNumbers {
            get {
                if (string.IsNullOrEmpty(this.CellNo))
                    return null;

                if (fCellNumbers == null)
                    fCellNumbers = new List<ICellNumber>() {
                    new CellNumber(this.Session) { CellNo = this.CellNo, FullName = this.FullName, RecipientRole = this.RecipientRole }
                };

                return fCellNumbers;
            }
        }

        private IEnumerable<IFileAttachment> fEmailAttachments;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IFileAttachment> EmailAttachments {
            get {
                if(fEmailAttachments == null)
                    fEmailAttachments = this.Attachments.ToList();

                return fEmailAttachments;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public KeyValuePair<string, string> KeyValuePair {
            get {
                return new KeyValuePair<string, string>(this.Email, this.FullName);
            }
        }

        [VisibleInListView(false),VisibleInDetailView(false)]
        public RecipientRoles RecipientRole {
            get {
                return RecipientRoles.VendorContact;
            }
        }
        #endregion

        #region Events
        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.Created = Constants.DateTimeTimeZone(this.Session);
            }
        }
        #endregion
    }
}
