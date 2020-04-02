using DevExpress.ExpressApp;
using DevExpress.Xpo;
using Ignyt.Framework.ExpressApp;

namespace MyWorkbench.BusinessObjects.Helpers.Notifications {
    public abstract class ExportHelperBase {
        private XafApplication _application;

        protected ExportHelperBase(XafApplication Application) {
            this._application = Application;
        }

        protected byte[] Export(IXPSimpleObject Object, string DisplayName) {
            using (ReportsV2Helper helper = new ReportsV2Helper(this._application, Object)) {
                return helper.ExportObject("Oid", DisplayName);
            }
        }
    }
}
