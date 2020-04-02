namespace Ignyt.BusinessInterface {
    public interface IWorkflowSignature<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
