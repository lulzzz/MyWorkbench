using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework.ExpressApp;
using MyWorkbench.BaseObjects.Constants;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Communication.Common;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using MyWorkbench.BusinessObjects.Utils;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Communication.Enum;
using Ignyt.BusinessInterface.Attributes;
using Ignyt.BusinessInterface.Communication;

namespace MyWorkbench.BusinessObjects.Communication
{
    [DefaultClassOptions]
    [DefaultProperty("Subject")]
    [ImageName("BO_Resume")]
    [NavigationItem("Communication")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Edit;New;Link;Unlink", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Email : BaseObject, IDetailRowMode, IEmail, IModal
    {
        public Email(Session session)
            : base(session)
        {
        }

        #region Properties
        private IEmailPopup fCurrentObject;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public IEmailPopup CurrentObject {
            get {
                return fCurrentObject;
            }
            set {
                SetPropertyValue("CurrentObject", ref fCurrentObject, value);
                this.ObjectOid = (fCurrentObject as BaseObject).Oid;
                this.ObjectType = this.fCurrentObject.GetType();
                this.CloneAttachments();
            }
        }

        private Guid fObjectOid;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Guid ObjectOid {
            get {
                return fObjectOid;
            }
            set {
                SetPropertyValue("ObjectOid", ref fObjectOid, value);
            }
        }

        private Type fObjectType;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type ObjectType {
            get {
                return fObjectType;
            }
            set {
                SetPropertyValue("ObjectType", ref fObjectType, value);
            }
        }

        private Party fParty;
        [VisibleInListView(true), VisibleInDetailView(false)]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private DateTime fCreated;
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime Created {
            get => fCreated;
            set => SetPropertyValue(nameof(Created), ref fCreated, value);
        }

        private DateTime? fSent;
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime? Sent {
            get => fSent;
            set => SetPropertyValue(nameof(Sent), ref fSent, value);
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("Email_VendorContact")]
        [EditorAlias("CustomAddTokenCollectionEditor")]
        [DevExpress.Xpo.DisplayName("To")]
        [RuleRequiredField(CustomMessageTemplate = "To cannot be empty", TargetContextIDs = "Immediate")]
        public XPCollection<VendorContact> ToEmailAddresses {
            get {
                return GetCollection<VendorContact>("ToEmailAddresses");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("CCEmail_VendorContact")]
        [EditorAlias("CustomAddTokenCollectionEditor")]
        [DevExpress.Xpo.DisplayName("CC")]
        public XPCollection<VendorContact> CCEmailAddresses {
            get {
                return GetCollection<VendorContact>("CCEmailAddresses");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("BCCEmail_VendorContact")]
        [EditorAlias("CustomAddTokenCollectionEditor")]
        [DevExpress.Xpo.DisplayName("BCC")]
        public XPCollection<VendorContact> BCCEmailAddresses {
            get {
                return GetCollection<VendorContact>("BCCEmailAddresses");
            }
        }

        private string fSubject;
        [RuleRequiredField(CustomMessageTemplate = "Subject cannot be empty", TargetContextIDs = "Immediate")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string Subject {
            get => fSubject;
            set => SetPropertyValue(nameof(Subject), ref fSubject, value);
        }

        private string fBody;
        [RuleRequiredField(CustomMessageTemplate = "Body cannot be empty", TargetContextIDs = "Immediate")]
        [EditorAlias("HtmlWithPlaceholders"), Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string Body {
            get => fBody;
            set => SetPropertyValue(nameof(Body), ref fBody, value);
        }

        private Template fTemplate;
        [DataSourceProperty("Templates")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ImmediatePostData]
        public Template Template {
            get => fTemplate;
            set => SetPropertyValue(nameof(Template), ref fTemplate, value);
        }
        private DateTime? fDateTimeToSend;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [EditorAlias("CustomDateTimeEditor")]
        public DateTime? DateTimeToSend {
            get => fDateTimeToSend;
            set => SetPropertyValue(nameof(DateTimeToSend), ref fDateTimeToSend, value);
        }


        private CommunicationNotificationStatus fStatus;
        [VisibleInListView(true), VisibleInDetailView(false)]
        public CommunicationNotificationStatus Status {
            get => fStatus;
            set => SetPropertyValue(nameof(Status), ref fStatus, value);
        }

        [VisibleInListView(false), VisibleInDetailView(true)]
        [Association("Email_EmailAttachment")]
        public XPCollection<EmailAttachment> EmailAttachments {
            get {
                return GetCollection<EmailAttachment>("EmailAttachments");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public BindingList<IEmailAttachment> Attachments {
            get {
                BindingList<IEmailAttachment> attachments = new BindingList<IEmailAttachment>();

                foreach (EmailAttachment emailAttachment in this.EmailAttachments)
                {
                    if(emailAttachment.IncludeInEmail)
                        attachments.Add(emailAttachment);
                }

                return attachments;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public BindingList<IEmailAddress> ToEmail {
            get {
                BindingList<IEmailAddress> emails = new BindingList<IEmailAddress>();

                foreach (VendorContact vendorContact in this.ToEmailAddresses)
                {
                    emails.AddRangeUnique(vendorContact.EmailAddresses);
                }

                return emails;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public BindingList<IEmailAddress> CCEmail {
            get {
                BindingList<IEmailAddress> emails = new BindingList<IEmailAddress>();

                foreach (VendorContact vendorContact in this.CCEmailAddresses)
                {
                    emails.AddRangeUnique(vendorContact.EmailAddresses);
                }

                return emails;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public BindingList<IEmailAddress> BCCEmail {
            get {
                BindingList<IEmailAddress> emails = new BindingList<IEmailAddress>();

                foreach (VendorContact vendorContact in this.BCCEmailAddresses)
                {
                    emails.AddRangeUnique(vendorContact.EmailAddresses);
                }

                return emails;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string Signature {
            get {
                if (this.Party != null)
                    return this.Session.FindObject<Employee>(CriteriaOperator.Parse("Oid ==?", this.Party.Oid)).Signature;
                return null;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string Footer {
            get {
                return string.Format("{0}{1}","Email sent using My Workbench. For more information visit us at ", "<a href=\"https://www.myworkbench.co.za\">My Workbench</a>");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        private IList<Template> Templates {
            get {
                return new XpoHelper(this.Session).GetObjects<Template>(CriteriaOperator.Parse("TemplateType == ? && CommunicationType = 1", this.CurrentObject.GetType())).ToList();
            }
        }

        private BindingList<EmailAddress> fEmailAddresses;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public BindingList<EmailAddress> EmailAddresses {
            get {
                if (CurrentObject != null & fEmailAddresses == null)
                {
                    fEmailAddresses = new BindingList<EmailAddress>();

                    if ((CurrentObject as IEmailPopup).EmailAddresses != null)
                    {
                        foreach (EmailAddress emailAddress in (CurrentObject as IEmailPopup).EmailAddresses)
                        {
                            fEmailAddresses.Add(emailAddress);
                        }
                    }
                }
                return fEmailAddresses;
            }
        }

        private BindingList<AttachmentBase> fAttachments;
        [VisibleInListView(false), VisibleInDetailView(false)]
        private BindingList<AttachmentBase> AttachmentBases {
            get {
                if (fAttachments == null)
                {
                    fAttachments = new BindingList<AttachmentBase>();

                    if ((CurrentObject as IEmailPopup).EmailAttachments != null)
                    {
                        foreach (AttachmentBase attachment in (CurrentObject as IEmailPopup).EmailAttachments)
                        {
                            if(attachment.IncludeInEmail)
                                fAttachments.Add(attachment);
                        }

                        AttachmentBase attachmentBase = BaseAttachment();

                        if(attachmentBase != null)
                            fAttachments.Add(attachmentBase);

                        attachmentBase = TextAttachment();

                        if (attachmentBase != null)
                            fAttachments.Add(attachmentBase);
                    }
                }
                return fAttachments;
            }
        }

        private IEmailAddress fFromEmail;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public IEmailAddress FromEmail {
            get {
                if (fFromEmail == null)
                    fFromEmail = new EmailAddress(this.Session) { Email = this.Session.FindObject<Employee>(CriteriaOperator.Parse("Oid ==?", this.Party.Oid)).Email, FullName = this.Party.DisplayName };
                return fFromEmail;
            }
        }

        private IEmailAddress fReplyTo;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public IEmailAddress ReplyTo {
            get {
                if(fReplyTo == null)
                    fReplyTo = new EmailAddress(this.Session) { Email = this.Session.FindObject<Employee>(CriteriaOperator.Parse("Oid ==?", this.Party.Oid)).Email, FullName = this.Party.DisplayName };
                return fReplyTo;
            }
        }

        private string fUniqueProviderIdentifier;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string UniqueProviderIdentifier {
            get => fUniqueProviderIdentifier;
            set => SetPropertyValue(nameof(UniqueProviderIdentifier), ref fUniqueProviderIdentifier, value);
        }

        [DevExpress.Xpo.DisplayName("Workflow")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public BaseObject BaseObject {
            get {
                return this.Session.FindObject(this.ObjectType, CriteriaOperator.Parse("Oid == ?", this.ObjectOid)) as BaseObject;
            }
        }
        #endregion

        #region Collections
        [DevExpress.Xpo.DisplayName("Logs")]
        [Association("Email_Log")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public XPCollection<CommunicationLog> CommunicationLog {
            get {
                return GetCollection<CommunicationLog>("CommunicationLog");
            }
        }
        #endregion

        #region Methods
        private AttachmentBase BaseAttachment()
        {
            if (!string.IsNullOrEmpty((CurrentObject as IEmailPopup).ReportDisplayName))
            {
                using (ReportsV2Helper helper = new ReportsV2Helper((BaseObject)this.CurrentObject))
                {
                    byte[] report = helper.ExportObject("Oid", (CurrentObject as IEmailPopup).ReportDisplayName);

                    FileData fileData = new FileData(this.Session);
                    fileData.LoadFromStream((CurrentObject as IEmailPopup).ReportFileName + ".pdf", new MemoryStream(report));

                    return new AttachmentBase(this.Session) { Attachment = fileData, Description = "Workflow to Email", IncludeInEmail = true };
                }
            }

            return null;
        }

        private AttachmentBase TextAttachment()
        {
            if ((CurrentObject as IEmailPopup).Text != null)
            {
                    FileData fileData = new FileData(this.Session);
                    fileData.LoadFromStream((CurrentObject as IEmailPopup).ReportFileName, new MemoryStream((CurrentObject as IEmailPopup).Text));

                    return new AttachmentBase(this.Session) { Attachment = fileData, Description = "Document to Email", IncludeInEmail = true };
            }

            return null;
        }

        private void CloneAttachments()
        {
            foreach (AttachmentBase attachment in this.AttachmentBases)
            {
                this.EmailAttachments.Add(new EmailAttachment(this.Session) { Attachment = attachment.Attachment, DateUploaded = Constants.DateTimeTimeZone(this.Session),
                    Description = attachment.Description, Email = this, ExpiryDate = attachment.ExpiryDate, IncludeInEmail = attachment.IncludeInEmail
                });
            }
        }
        #endregion

        #region Events
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "Template")
                if (oldValue != newValue & newValue != null)
                {
                    this.Subject = DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor.ReplacePlaceholders(this.Template.Subject, CurrentObject).Replace("&nbsp;", " ");
                    this.Body = string.Format("{0}{1}{2}", DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor.ReplacePlaceholders(this.Template.Body, CurrentObject).Replace("&nbsp;", " "), Environment.NewLine, this.Signature);
                }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
                
                this.Body = this.Signature;
                this.Created = Constants.DateTimeTimeZone(this.Session);
                this.Status = CommunicationNotificationStatus.Queued;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
        #endregion
    }
}
