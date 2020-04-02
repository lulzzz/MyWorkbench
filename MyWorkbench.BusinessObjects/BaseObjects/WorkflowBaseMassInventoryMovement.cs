using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Inventory;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkflowBaseMassInventoryMovement : BaseObject, IWorkflowBaseMassInventoryMovement<WorkflowBase>
    {
        public WorkflowBaseMassInventoryMovement(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkflowBaseMassInventoryMovement")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private MassInventoryMovement fMassInventoryMovement;
        [Association("MassInventoryMovement_WorkflowBaseMassInventoryMovement")]
        public MassInventoryMovement MassInventoryMovement {
            get {
                return fMassInventoryMovement;
            }
            set {
                SetPropertyValue("MassInventoryMovement", ref fMassInventoryMovement, value);
            }
        }
    }
}
