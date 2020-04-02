using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using System.Web.UI.WebControls;

namespace MyWorkbench.Module.Web.Controllers
{
    public class CustomizePopupSizeController : WindowController
    {
        public CustomizePopupSizeController()
        {
            this.TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ((WebApplication)Application).PopupWindowManager.PopupShowing += PopupWindowManager_PopupShowing;
        }

        private void PopupWindowManager_PopupShowing(object sender, PopupShowingEventArgs e)
        {
            e.PopupControl.CustomizePopupWindowSize += XafPopupWindowControl_CustomizePopupWindowSize;
        }

        private void XafPopupWindowControl_CustomizePopupWindowSize(object sender, DevExpress.ExpressApp.Web.Controls.CustomizePopupWindowSizeEventArgs e)
        {
            e.Width = new Unit(1280);
            e.Height = new Unit(600);
            e.Handled = true;
        }
    }
}
