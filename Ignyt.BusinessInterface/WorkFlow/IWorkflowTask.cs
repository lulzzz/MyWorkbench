namespace Ignyt.BusinessInterface {
    public interface IWorkflowTask<T> where T : IWorkflow {
        T Workflow { get; set; }
    }

    public interface IWorkFlowTimeTracking<T> where T : IWorkflow
    {
        T Workflow { get; set; }
    }
}
