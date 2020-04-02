using DevExpress.Persistent.BaseImpl;
using System;

namespace Ignyt.BusinessInterface {
    public interface IWorkflowTracking<T> where T : IWorkflow {
        T Workflow { get; set; }
        Party Party { get; set; }
        DateTime Processed { get; set; }
    }
}
