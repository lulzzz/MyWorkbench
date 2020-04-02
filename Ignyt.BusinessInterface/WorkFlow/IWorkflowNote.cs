namespace Ignyt.BusinessInterface {
    public interface IWorkflowNote<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
