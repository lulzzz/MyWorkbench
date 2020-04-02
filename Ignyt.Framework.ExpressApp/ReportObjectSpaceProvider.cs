using System;
using System.Collections.Generic;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.ExpressApp;

namespace Ignyt.Framework.ExpressApp {
    public class ReportObjectSpaceProvider : IReportObjectSpaceProvider, IDisposable {
        IObjectSpace objectSpace;

        public ReportObjectSpaceProvider(IObjectSpace objectSpace) {
            this.objectSpace = objectSpace;
        }

        public void Dispose() {
            if (objectSpace != null) {
                objectSpace.Dispose();
                objectSpace = null;
            }
        }

        public void DisposeObjectSpaces() {
            if (objectSpace != null) {
                objectSpace.Dispose();
                objectSpace = null;
            }
        }

        public IObjectSpace GetObjectSpace(Type type) {
            return objectSpace;
        }
    }
}
