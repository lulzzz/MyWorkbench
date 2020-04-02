using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects {
    [DefaultClassOptions]
    [NavigationItem("Work Flow")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Ticket : WorkflowBase, ICustomizable, IDetailRowMode
    {
        public Ticket(Session session)
            : base(session) {
        }

        #region Properties
        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.Ticket;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Ticket";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Ticket - " + this.No;
            }
        }
        #endregion

        protected override void OnSaved()
        {
            new WorkFlowExecute<Ticket>().Execute(this);

            base.OnSaved();
        }
    }
}
