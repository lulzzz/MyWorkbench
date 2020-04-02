using System;
using System.Configuration;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using MyWorkbench.Module.Web.Helpers;
using MyWorkbench.Web.Helpers;
using System.Web.Routing;
using DevExpress.Security.Resources;
using DevExpress.XtraReports.Security;

namespace MyWorkbench.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e) {
            SecurityAdapterHelper.Enable();
            RouteTable.Routes.RegisterXafRoutes();
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton<WebDocumentViewerOperationLogger, ViewerOperationLogger>();
            DevExpress.Security.Resources.AccessSettings.ReportingSpecificResources.SetRules(DirectoryAccessRule.Allow("C:\\StaticResources\\"), SerializationFormatRule.Deny(DevExpress.XtraReports.UI.SerializationFormat.CodeDom));
        }

        protected void Session_Start(Object sender, EventArgs e) {
            Tracing.Initialize();

            WebApplication.SetInstance(Session, new MyWorkbenchAspNetApplication());
            WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath = "ResponsiveVerticalTemplateContent.ascx";
            DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            WebApplication.Instance.SwitchToNewStyle();
            WebApplicationStyleManager.EnableUpperCase = false;

            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }

            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }

        protected void Application_BeginRequest(Object sender, EventArgs e) {
        }

        protected void Application_EndRequest(Object sender, EventArgs e) {
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
        }

        protected void Application_Error(Object sender, EventArgs e) {
            if (ErrorHandling.GetApplicationError() != null)
                ToastMessageHelper.ShowErrorMessage(WebApplication.Instance, ErrorHandling.GetApplicationError().Exception, InformationPosition.Bottom);

            else if (Server.GetLastError() != null)
                ToastMessageHelper.ShowErrorMessage(WebApplication.Instance, Server.GetLastError(), InformationPosition.Bottom);


            ErrorHandling.ClearApplicationError();
            Server.ClearError();
        }

        protected void Session_End(Object sender, EventArgs e) {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }

        protected void Application_End(Object sender, EventArgs e) {
        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
