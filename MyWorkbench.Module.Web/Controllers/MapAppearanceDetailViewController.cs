using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;

namespace MyWorkbench.Module.Web.Controllers {
    public class MapAppearanceDetailViewController : ObjectViewController<DetailView, IMapsMarker> {
        private AppearanceController controller = null;

        public MapAppearanceDetailViewController() {}

        protected override void OnActivated() {
            base.OnActivated();

            controller = Frame.GetController<AppearanceController>();

            if (controller != null)
                controller.CustomApplyAppearance += Controller_CustomApplyAppearance;
        }

        private void Controller_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e) {
            if (e.ItemName == "Self" && View.ViewEditMode == ViewEditMode.View) {
                foreach (IConditionalAppearanceItem item in e.AppearanceItems) {
                    if (item is AppearanceItemVisibility) {
                        ((AppearanceItemVisibility)item).Visibility = ViewItemVisibility.Show;
                    }
                }
            }
        }

        protected override void OnDeactivated() {
            base.OnDeactivated();

            if (controller != null) {
                controller.CustomApplyAppearance -= Controller_CustomApplyAppearance;
            }
        }
    }
}
