using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.SystemModule;

namespace MyWorkbench.Module.Web.Controllers {
    public partial class DetailViewGlobalSettingsController : ViewController<DetailView> {
        protected override void OnActivated() {
            base.OnActivated();
            View.ViewEditModeChanged += View_ViewEditModeChanged;
            SetCollectionsEditMode();
        }

        protected override void OnDeactivated() {
            View.ViewEditModeChanged -= View_ViewEditModeChanged;
            base.OnDeactivated();
        }

        private void View_ViewEditModeChanged(object sender, System.EventArgs e) {
            SetCollectionsEditMode();
        }

        private void SetCollectionsEditMode() {
            if (View.Model is IModelDetailViewWeb modelDetailView && modelDetailView.CollectionsEditMode != View.ViewEditMode) {
                modelDetailView.CollectionsEditMode = View.ViewEditMode;
                UpdateController<WebLinkUnlinkController>();
                UpdateController<WebNewObjectViewController>();
                UpdateController<WebModificationsController>();
                UpdateController<WebDeleteObjectsViewController>();
            }
        }

        private void UpdateController<T>() where T : ViewController {
            T controller = Frame.GetController<T>();
            if (controller != null) {
                controller.Active["TEMP"] = false;
                controller.Active.RemoveItem("TEMP");
            }
        }
    }
}
