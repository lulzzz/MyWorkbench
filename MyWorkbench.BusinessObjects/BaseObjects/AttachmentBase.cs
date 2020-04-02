using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Communication;
using MyWorkbench.BaseObjects.Constants;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    [DefaultProperty("Description")]
    public class AttachmentBase : BaseObject, IFileAttachment, IEmailAttachment, IModal
    {
        public AttachmentBase(Session session)
            : base(session) {
        }

        private FileData fAttachment;
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [FileTypeFilter("All Files", 2, "*.*")]
        [RuleRequiredField(DefaultContexts.Save)]
        public FileData Attachment {
            get => fAttachment;
            set => SetPropertyValue(nameof(Attachment), ref fAttachment, value);
        }

        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private DateTime? fExpiryDate;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public DateTime? ExpiryDate {
            get => fExpiryDate;
            set => SetPropertyValue(nameof(ExpiryDate), ref fExpiryDate, value);
        }

        private DateTime? fDateUploaded;
        [VisibleInDetailView(false), VisibleInListView(true)]
        public DateTime? DateUploaded {
            get => fDateUploaded;
            set => SetPropertyValue(nameof(DateUploaded), ref fDateUploaded, value);
        }

        private bool fIncludeInEmail;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public bool IncludeInEmail {
            get => fIncludeInEmail;
            set => SetPropertyValue(nameof(IncludeInEmail), ref fIncludeInEmail, value);
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Content {
            get {
                return Convert.ToBase64String(Attachment.Content);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Type {
            get {
                return null;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string FileName {
            get {
                return Attachment.FileName;
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            this.DateUploaded = Constants.DateTimeTimeZone(this.Session);
        }
    }
}
