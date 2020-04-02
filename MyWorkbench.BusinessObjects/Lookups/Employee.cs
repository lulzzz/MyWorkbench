using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Communication.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.IO;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects.Lookups {
    [NavigationItem("People")]
    [ImageName("BO_Customer")]
    public class Employee : Person, ISecurityUser, IAuthenticationStandardUser, 
        IAuthenticationActiveDirectoryUser, ISecurityUserWithRoles, IPermissionPolicyUser, 
        IEmailAddress, ICellNumber, IEmailPopup, IMessagePopup, IRecipientType, IWorkflowDesign
    {
        public Employee(Session session)
            : base(session) {
        }

        #region Properties
        [Association("Employee_WorkflowBase")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<WorkflowBase> Workflow {
            get {
                return GetCollection<WorkflowBase>("Workflow");
            }
        }

        private EmployeeType fEmployeeType;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true),VisibleInListView(true)]
        [ModelDefault("Index", "1")]
        public EmployeeType EmployeeType {
            get => fEmployeeType;
            set => SetPropertyValue(nameof(EmployeeType), ref fEmployeeType, value);
        }

        private Country fCountry;
        [VisibleInDetailView(true), VisibleInListView(false)]
        public Country Country {
            get => fCountry;
            set => SetPropertyValue(nameof(Country), ref fCountry, value);
        }

        private string fEmployeeNo;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("Index", "4")]
        public string EmployeeNo {
            get => fEmployeeNo;
            set => SetPropertyValue(nameof(EmployeeNo), ref fEmployeeNo, value);
        }

        private string fIDNumber;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("ID No")]
        public string IDNumber {
            get => fIDNumber;
            set => SetPropertyValue(nameof(IDNumber), ref fIDNumber, value);
        }

        private string fPassportNumber;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Passport No")]
        public string PassportNumber {
            get => fPassportNumber;
            set => SetPropertyValue(nameof(PassportNumber), ref fPassportNumber, value);
        }

        private string fPermitNumber;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Permit No")]
        public string PermitNumber {
            get => fPermitNumber;
            set => SetPropertyValue(nameof(PermitNumber), ref fPermitNumber, value);
        }

        private DateTime fPermitExpiry;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public DateTime PermitExpiry {
            get => fPermitExpiry;
            set => SetPropertyValue(nameof(PermitExpiry), ref fPermitExpiry, value);
        }

        private string fCellNo;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        public string CellNo {
            get => fCellNo;
            set => SetPropertyValue(nameof(CellNo), ref fCellNo, value);
        }

        private string fNotes;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "10")]
        public string Notes {
            get => fNotes;
            set => SetPropertyValue(nameof(Notes), ref fNotes, value);
        }

        private WorkflowResource fResource;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public WorkflowResource WorkflowResource {
            get => fResource;
            set => SetPropertyValue(nameof(WorkflowResource), ref fResource, value);
        }

        [Persistent("Color")]
        private Int32 fColor;
        [NonPersistent]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("Index", "5")]
        public System.Drawing.Color Color {
            get => System.Drawing.Color.FromArgb(fColor);
            set => SetPropertyValue(nameof(Color), ref fColor, value.ToArgb());
        }

        private DateTime? fResigned;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public DateTime? Resigned {
            get => fResigned;
            set => SetPropertyValue(nameof(Resigned), ref fResigned, value);
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("Index", "7")]
        public double RatePerHour {
            get {
                if (this.EmployeeRates.Count == 0)
                    return 0;

                if (this.EmployeeRates.OrderByDescending(g => g.ValidFrom).Where(g => g.ValidFrom <= Constants.DateTimeTimeZone(this.Session).Date).Count() == 0)
                    return 0;

                return this.EmployeeRates.OrderByDescending(g => g.ValidFrom).Where(g => g.ValidFrom <= Constants.DateTimeTimeZone(this.Session).Date).First().RatePerHour;
            }
        }

        private FileData fFingerPrint;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "8")]
        public FileData FingerPrint {
            get => fFingerPrint;
            set => SetPropertyValue(nameof(FingerPrint), ref fFingerPrint, value);
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

        private string fDescription;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private string fSkillType;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string SkillType {
            get => fSkillType;
            set => SetPropertyValue(nameof(SkillType), ref fSkillType, value);
        }
        #endregion

        #region Property Overides
        [VisibleInDetailView(true), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public new string FirstName {
            get { return base.FirstName; }
            set { base.FirstName = value; }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public new string LastName {
            get { return base.LastName; }
            set { base.LastName = value; }
        }
        #endregion

        #region ISecurityUser Members
        private bool fIsActive = true;
        [VisibleInListView(true),VisibleInDetailView(true)]
        [ModelDefault("Index", "9")]
        public bool IsActive {
            get => fIsActive;
            set => SetPropertyValue(nameof(IsActive), ref fIsActive, value);
        }

        private string fUserName = String.Empty;
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save, 
            TargetCriteria = "[EmployeeType] = ##Enum#Ignyt.BusinessInterface.EmployeeType,SystemUser#")]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
            "The login with the entered user name was already registered within the system.", TargetCriteria = "[EmployeeType] = ##Enum#Ignyt.BusinessInterface.EmployeeType,SystemUser#")]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Email, CustomMessageTemplate = RegularExpressions.EmailError)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "10")]
        public string UserName {
            get => fUserName;
            set => SetPropertyValue(nameof(UserName), ref fUserName, value);
        }

        private string fSignature;
        [Size(SizeAttribute.Unlimited)]
        [ToolTip("Your signature will be appended to outgoing communication")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.HtmlPropertyEditor.Web.ASPxHtmlPropertyEditor")]
        public string Signature {
            get => fSignature;
            set => SetPropertyValue(nameof(Signature), ref fSignature, value);
        }

        private string fRandomPassword;
        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string RandomPassword {
            get {
                if (fRandomPassword == null)
                    fRandomPassword = Guid.NewGuid().ToString("d").Substring(1, 7);
                return fRandomPassword;
            }
        }
        #endregion

        #region IAuthenticationStandardUser Members
        private bool fChangePasswordOnFirstLogon;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public bool ChangePasswordOnFirstLogon {
            get => fChangePasswordOnFirstLogon;
            set => SetPropertyValue(nameof(ChangePasswordOnFirstLogon), ref fChangePasswordOnFirstLogon, value);
        }

        private string fStoredPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        public string StoredPassword {
            get => fStoredPassword;
            set => SetPropertyValue(nameof(StoredPassword), ref fStoredPassword, value);
        }

        [Browsable(false), SecurityBrowsable]
        public string DecryptedStoredPassword {
            get { return TextEncryption.Decypt(this.StoredPassword); }
        }

        public bool ComparePassword(string password)
        {
            if (TextEncryption.Decypt(this.StoredPassword) == password)
                return true;
            else
                return false;
        }

        public void SetPassword(string password) {
            this.StoredPassword = TextEncryption.Encrypt(password);
        }
        #endregion

        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles {
            get {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (EmployeeRole role in EmployeeRoles) {
                    result.Add(role);
                }
                return result;
            }
        }
        #endregion

        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles {
            get { return EmployeeRoles.OfType<IPermissionPolicyRole>(); }
        }
        #endregion

        #region Collections
        [Association("Employee_EmployeeRole")]
        [RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
        TargetCriteria = "IsActive and [EmployeeType] = ##Enum#Ignyt.BusinessInterface.EmployeeType,SystemUser#",
        CustomMessageTemplate = "An active employee must have at least one role assigned")]
        public XPCollection<EmployeeRole> EmployeeRoles {
            get {
                return GetCollection<EmployeeRole>("EmployeeRoles");
            }
        }

        [Association("Employee_FileAttachment")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<FileAttachment> Attachments {
            get {
                return GetCollection<FileAttachment>("Attachments");
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("Employee_EmployeeRate")]
        public XPCollection<EmployeeRate> EmployeeRates {
            get {
                return GetCollection<EmployeeRate>("EmployeeRates");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [Association("Employee_WorkFlowTimeTrackingMultiple")]
        public XPCollection<WorkFlowTimeTrackingMultiple> WorkFlowTimeTrackingMultiples {
            get {
                return GetCollection<WorkFlowTimeTrackingMultiple>("WorkFlowTimeTrackingMultiples");
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
                return new List<IEmailAddress> {
                    new EmailAddress(this.Session) { Email = this.Email, FullName = this.FullName, RecipientRole = this.RecipientRole }
                };
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<ICellNumber> CellNumbers {
            get {
                if (string.IsNullOrEmpty(this.CellNo))
                    return null;

                return new List<ICellNumber> {
                    new CellNumber(this.Session) { CellNo = this.CellNo, FullName = this.FullName, RecipientRole = this.RecipientRole  }
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
        public string ReportDisplayName { get { return string.Empty; } }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string ReportFileName { get { return string.Empty; } }

        private byte[] ftext = null;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonPersistent]
        public byte[] Text { get { return ftext; } set { ftext = value; } }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public RecipientRoles RecipientRole {
            get {
                if(EmployeeType == EmployeeType.Employee)
                    return RecipientRoles.Employee;
                else if (EmployeeType == EmployeeType.Driver)
                    return RecipientRoles.Driver;
                else if (EmployeeType == EmployeeType.SystemUser)
                    return RecipientRoles.SystemUser;

                return RecipientRoles.Employee;
            }
        }
        #endregion

        #region Events
        protected override void OnSaving() {
            if (this.WorkflowResource == null)
                this.WorkflowResource = new WorkflowResource(this.Session) { Caption = this.FullName, Color = this.Color,
                    ResourceType = this.GetType(), ObjectOid = this.Oid
                };
            else {
                this.WorkflowResource.ResourceType = this.GetType();
                this.WorkflowResource.ObjectOid = this.Oid;
                this.WorkflowResource.Caption = this.FullName;
                this.WorkflowResource.Color = this.Color;
            }

            if(this.Photo != null)
                this.WorkflowResource.Image = Image.FromStream(new MemoryStream(this.Photo));

            WorkFlowHelper.CreateWorkFlowProcess(this);

            base.OnSaving();
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId != null && SecuritySystem.CurrentUserId.ToString() != string.Empty)
                {
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                    this.Created = this.Session == null ? DateTime.Now : Constants.DateTimeTimeZone(this.Session);
                }
                else{
                    this.Created = DateTime.Now;
                }
            }
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<Employee>().Execute(this);

            base.OnSaved();
        }
        #endregion
    }
}
