using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Common;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkflowType : BaseObject, IWorkflowType
    {
        public WorkflowType(Session session)
            : base(session) {
        }

        private string fDescription;
        [Size(200)]
        [RuleUniqueValue]
        [Indexed(Unique = true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true),VisibleInDetailView(true)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private bool fReduceInventory;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public bool ReduceInventory {
            get {
                return fReduceInventory;
            }
            set {
                SetPropertyValue("ReduceInventory", ref fReduceInventory, value);
            }
        }


        [VisibleInListView(false),VisibleInDetailView(false)]
        [Association("WorkflowType_WorkflowBase")]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [Association("WorkflowType_Checklist")]
        public XPCollection<Checklist> Checklists {
            get {
                return GetCollection<Checklist>("Checklists");
            }
        }
    }
}
