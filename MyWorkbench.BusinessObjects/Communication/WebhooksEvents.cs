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
    public class WebhooksEvents : BaseObject
    {
        public WebhooksEvents(Session session)
            : base(session)
        {
        }

        private string fInternalMessageId;
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string InternalMessageId {
            get => fInternalMessageId;
            set => SetPropertyValue(nameof(InternalMessageId), ref fInternalMessageId, value);
        }

        private string fEmail;
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string Email {
            get => fEmail;
            set => SetPropertyValue(nameof(Email), ref fEmail, value);
        }

        private string fEventType;
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string EventType {
            get => fEventType;
            set => SetPropertyValue(nameof(EventType), ref fEventType, value);
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

        private DateTime? fExecuted;
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime? Executed {
            get => fExecuted;
            set => SetPropertyValue(nameof(Executed), ref fExecuted, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.Created = Constants.DateTimeTimeZone(this.Session);
        }
    }
}
