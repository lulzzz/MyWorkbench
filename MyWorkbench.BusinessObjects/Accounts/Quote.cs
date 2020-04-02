using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects.Accounts {
    [DefaultClassOptions]
    [NavigationItem("Accounts")]
    [ImageName("BO_Resume")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Quote : WorkflowBase, ICustomizable, IDetailRowMode
    {
        public Quote(Session session)
            : base(session) {
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.Quote;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Quote";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Quote - " + this.No;
            }
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<Quote>().Execute(this);

            base.OnSaved();
        }
    }
}
