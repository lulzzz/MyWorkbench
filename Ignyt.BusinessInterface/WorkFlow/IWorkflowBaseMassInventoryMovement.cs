using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignyt.BusinessInterface {
    public interface IWorkflowBaseMassInventoryMovement<T> where T : IWorkflow {
        T Workflow { get; set; }
    }
}
