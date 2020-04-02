namespace Ignyt.BusinessInterface {
    public interface IWorkflowAttachment<T> where T : IWorkflow {
        T Workflow { get; set; }
    }

    public interface IWorkFlowChecklist<T> where T : IWorkflow
    {
        T Workflow { get; set; }
    }
}
