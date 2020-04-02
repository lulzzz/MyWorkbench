using DevExpress.Persistent.BaseImpl;
using System;

namespace Ignyt.BusinessInterface {
    public interface IWorkflowMessage<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
