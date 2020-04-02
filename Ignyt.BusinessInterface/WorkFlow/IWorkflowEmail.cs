using DevExpress.Persistent.BaseImpl;
using System;

namespace Ignyt.BusinessInterface {
    public interface IWorkflowEmail<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
