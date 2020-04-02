using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Communication;
using System;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.Office
{
    [DefaultClassOptions]
    [NavigationItem("Office")]
    [ImageName("BO_Resume")]
    public class Spreadsheet : BaseObject, IEmailPopup
    {
        public Spreadsheet(Session session)
            : base(session)
        {
        }

        private string fDescription;
        [VisibleInListView(true),VisibleInDetailView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [NonCloneable]
        public string Description {
            get { return fDescription; }
            set { SetPropertyValue(nameof(Description), ref fDescription, value); }
        }

        private byte[] fText;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.SpreadsheetPropertyEditor)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("")]
        public byte[] Text {
            get { return fText; }
            set { SetPropertyValue(nameof(Text), ref fText, value); }
        }

        private DateTime fDateTime;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(false), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        [NonCloneable]
        public DateTime DateTime {
            get => fDateTime;
            set => SetPropertyValue(nameof(DateTime), ref fDateTime, value);
        }

        private Party fParty;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Created By")]
        [NonCloneable]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        [NonCloneable]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<Email> Emails {
            get {
                SortingCollection sortCollection = new SortingCollection
                {
                    new SortProperty("Created", SortingDirection.Descending)
                };

                XPCollection<Email> emails = new XPCollection<Email>(this.Session, CriteriaOperator.Parse("ObjectOid = ?", this.Oid))
                {
                    Sorting = sortCollection
                };

                return emails;
            }
        }

        private Guid fApplicationID;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Guid ApplicationID {
            get {
                if (fApplicationID == Guid.Empty)
                    fApplicationID = MyWorkbench.BaseObjects.Constants.Constants.ApplicationID(this.Session);
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
                return this.Description;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IEmailAddress> EmailAddresses {
            get {
                return null;
            }
        }

        private IEnumerable<IFileAttachment> fEmailAttachments;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IFileAttachment> EmailAttachments {
            get {
                if (fEmailAttachments == null)
                {
                    List<IFileAttachment> items = new List<IFileAttachment>();
                    //items.AddRange(this.Attachments);

                    fEmailAttachments = items;
                }
                return fEmailAttachments;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string ReportDisplayName { get { return null; } }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string ReportFileName { get { return string.Format("{0}{1}", this.Description, ".xlsx"); } }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);


                this.DateTime = MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(this.Session);
            }
        }
    }
}
