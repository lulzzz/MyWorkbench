using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.SystemModule;

namespace MyWorkbench.Module.Web.Controllers {
    public class DashboardController : DashboardCreationWizardController {
        public DashboardController() {
        }

        protected override void OnActivated() {
            base.OnActivated();

            this.Active.SetItemValue("Hide", false);
        }
    }

    public class DashboardCustomizeController : DashboardCustomizationController
    {
        public DashboardCustomizeController()
        {
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            this.Active.SetItemValue("Hide", false);
        }
    }
}
