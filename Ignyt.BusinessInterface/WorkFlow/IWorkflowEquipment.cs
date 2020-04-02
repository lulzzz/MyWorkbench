namespace Ignyt.BusinessInterface {
    public interface IWorkflowEquipment<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
