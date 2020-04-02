using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.SystemModule;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.Module.Web.Controllers {
    public class VendorController : ObjectViewController<DetailView, Vendor> {
        protected override void OnActivated() {
            base.OnActivated();

            object currentObject = View.CurrentObject;

            if (ObjectSpace.IsNewObject(currentObject))
            {
                if (View.Id.Contains("Supplier"))
                    (currentObject as Vendor).AccountType = Ignyt.BusinessInterface.VendorType.Supplier;
            }
        }
    }
}
