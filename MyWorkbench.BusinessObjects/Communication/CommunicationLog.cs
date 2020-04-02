using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Ignyt.BusinessInterface.Attributes;
using Ignyt.BusinessInterface;

namespace MyWorkbench.BusinessObjects.Communication
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [DefaultProperty("Description")]
    [ImageName("BO_Resume")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Edit;New;Link;Unlink;Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class CommunicationLog : BaseObject, IModal
    {
        public CommunicationLog(Session session)
            : base(session)
        {
        }

        private Email fEmail;
        [Association("Email_Log")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Email Email {
            get => fEmail;
            set => SetPropertyValue(nameof(Email), ref fEmail, value);
        }

        private Message fMessage;
        [Association("Message_Log")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Message Message {
            get => fMessage;
            set => SetPropertyValue(nameof(Message), ref fMessage, value);
        }

        private DateTime fCreated;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime Created {
            get => fCreated;
            set => SetPropertyValue(nameof(Created), ref fCreated, value);
        }

        private string fDescription;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Size(SizeAttribute.Unlimited)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private string fCurrentStatus;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string CurrentStatus {
            get => fCurrentStatus;
            set => SetPropertyValue(nameof(CurrentStatus), ref fCurrentStatus, value);
        }

        private string fMessageIdenfifier;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string MessageIdenfifier {
            get => fMessageIdenfifier;
            set => SetPropertyValue(nameof(MessageIdenfifier), ref fMessageIdenfifier, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.Created = Constants.DateTimeTimeZone(this.Session);
        }
    }
}
