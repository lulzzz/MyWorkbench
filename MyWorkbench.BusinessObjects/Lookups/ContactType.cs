using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Employee")]
    [Appearance("HideActions", AppearanceItemType = "Action", Criteria = "[SystemGenerated]", TargetItems = "Edit;SwitchToEditMode;Link;Unlink;Delete;", Enabled = false, Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class ContactType : BaseObject, IRecipientType {
        public ContactType(Session session)
            : base(session) {
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private bool fSystemGenerated;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool SystemGenerated {
            get => fSystemGenerated;
            set => SetPropertyValue(nameof(SystemGenerated), ref fSystemGenerated, value);
        }
    }
}
