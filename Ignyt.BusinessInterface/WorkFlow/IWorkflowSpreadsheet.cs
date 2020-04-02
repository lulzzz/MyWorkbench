using DevExpress.Xpo;

namespace Ignyt.BusinessInterface {
    public interface IWorkflowSpreadsheet<T> where T : IWorkflow {
        XPCollection<T> Workflow { get; }
    }
}
