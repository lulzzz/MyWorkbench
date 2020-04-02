using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowAttachment : AttachmentBase, IWorkflowAttachment<WorkflowBase>, IModal
    {
        public WorkFlowAttachment(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowAttachment")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private WorkFlowTask fWorkFlowTask;
        [Association("WorkFlowTask_WorkFlowAttachment")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WorkFlowTask WorkFlowTask {
            get { return fWorkFlowTask; }
            set {
                SetPropertyValue("WorkFlowTask", ref fWorkFlowTask, value);
            }
        }
    }
}
