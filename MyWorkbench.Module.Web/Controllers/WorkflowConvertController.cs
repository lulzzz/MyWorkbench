using DevExpress.ExpressApp;
using System;
using MyWorkbench.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace MyWorkbench.Module.Web.Controllers {
    public class CustomStatusColourController : ViewController<ListView> {
        private AppearanceController appearanceController;

        public CustomStatusColourController() {
            TargetObjectType = typeof(IStatusPriority<Status,Priority>);
        }

        protected override void OnActivated() {
            base.OnActivated();

            appearanceController = Frame.GetController<AppearanceController>();
            if (appearanceController != null) {
                appearanceController.CustomApplyAppearance += new EventHandler<ApplyAppearanceEventArgs>(appearanceController_CustomApplyAppearance);
            }
        }

        void appearanceController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e) {
            if (e.ContextObjects == null || e.ContextObjects.Length != 1) return;
            IStatusPriority<Status, Priority> obj = e.ContextObjects[0] as IStatusPriority<Status, Priority>;
            if (obj == null) return;
            if (obj.Status == null) return;
            //only for- listView
            if (View is ListView)
                e.AppearanceObject.FontColor = obj.Status.Color;
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated() {
            if (appearanceController != null) {
                appearanceController.AppearanceApplied -= new EventHandler<ApplyAppearanceEventArgs>(appearanceController_CustomApplyAppearance);
            }
            base.OnDeactivated();
        }
    }
}
