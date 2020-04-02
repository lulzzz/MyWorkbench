using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Communication.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [NavigationItem("People")]
    [DefaultProperty("Description")]
    [ImageName("BO_Customer")]
    public class Team : BaseObject, IEmailAddress, ICellNumber, IEmailPopup, IMessagePopup, IRecipientType
    {
        public Team(Session session)
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

        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true),VisibleInListView(true)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private string fEmail;
        [ToolTip("The primary email address used for sending correspondence to the team")]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Email, CustomMessageTemplate = RegularExpressions.EmailError)]
        [VisibleInDetailView(true), VisibleInListView(false)]
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
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.Phone, CustomMessageTemplate = RegularExpressions.PhoneError)]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public string CellNo {
            get => fCellNo;
            set => SetPropertyValue(nameof(CellNo), ref fCellNo, value);
        }

        private string fArea;
        [ToolTip("The area will be displayed alongside the team name in the team dropdown for easy reference")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Area {
            get {
                return fArea;
            }
            set {
                SetPropertyValue("Area", ref fArea, value);
            }
        }

        private TeamType fType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public TeamType Type {
            get {
                return fType;
            }
            set {
                SetPropertyValue("Type", ref fType, value);
            }
        }

        private WorkflowResource fResource;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public WorkflowResource WorkflowResource {
            get {
                return this.fResource;
            }
            set {
                this.fResource = value;
            }
        }

        [Persistent("Color")]
        private Int32 color;
        [NonPersistent]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public System.Drawing.Color Color {
            get { return System.Drawing.Color.FromArgb(color); }
            set {
                color = value.ToArgb();
                OnChanged("Color");
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
        [Association("Team_FileAttachment")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<FileAttachment> Attachments {
            get {
                return GetCollection<FileAttachment>("Attachments");
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
        public string FullName {
            get {
                return this.Description;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IEmailAddress> EmailAddresses {
            get {
                return new List<IEmailAddress> {
                    new EmailAddress(this.Session) { Email = this.Email, FullName = this.Description, RecipientRole = this.RecipientRole  }
                };
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<ICellNumber> CellNumbers {
            get {
                if (string.IsNullOrEmpty(this.CellNo))
                    return null;

                return new List<ICellNumber> {
                    new CellNumber(this.Session) { CellNo = this.CellNo, FullName = this.Description, RecipientRole = this.RecipientRole  }
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
        public KeyValuePair<string,string> KeyValuePair {
            get {
                return new KeyValuePair<string, string>(this.Email,this.FullName);
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
                return RecipientRoles.Team;
            }
        }
        #endregion

        #region Events
        protected override void OnSaving() {
            if (this.WorkflowResource == null)
                this.WorkflowResource = new WorkflowResource(this.Session) { Caption = this.Description, Color = this.Color,
                    ResourceType = this.GetType(), ObjectOid = this.Oid, Image = this.Image };
            else {
                this.WorkflowResource.ResourceType = this.GetType();
                this.WorkflowResource.ObjectOid = this.Oid;
                this.WorkflowResource.Caption = this.Description;
                this.WorkflowResource.Color = this.Color;
                this.WorkflowResource.Image = this.Image;
            }

            base.OnSaving();
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.Created = Constants.DateTimeTimeZone(this.Session);
            }
        }

        protected override void OnDeleting() {
            this.WorkflowResource.Delete();
            base.OnDeleting();
        }
        #endregion
    }
}
