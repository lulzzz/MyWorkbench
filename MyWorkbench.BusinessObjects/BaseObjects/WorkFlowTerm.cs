using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.BaseObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [ImageName("Action_Debug_Stop")]
    public class WorkFlowTerm : BaseObject {
        public WorkFlowTerm(Session session)
            : base(session) {
        }

        private string fName;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Name {
            get => fName;
            set => SetPropertyValue(nameof(Name), ref fName, value);
        }

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "5")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        [Association("WorkFlowTerm_WorkFlowBase")]
        [DevExpress.Xpo.DisplayName("Terms")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<WorkflowBase> WorkflowBase {
            get {
                return GetCollection<WorkflowBase>("WorkflowBase");
            }
        }
    }
}
