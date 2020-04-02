namespace Ignyt.BusinessInterface {
    public interface IWorkflowImage<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
