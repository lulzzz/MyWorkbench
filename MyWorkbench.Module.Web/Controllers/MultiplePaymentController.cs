using DevExpress.ExpressApp;
using MyWorkbench.BusinessObjects.Accounts;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;

namespace MyWorkbench.Module.Web.Controllers
{
    public class MultiplePaymentViewController : ObjectViewController<ListView, MultiplePayment>
    {
        private System.ComponentModel.IContainer components = null;

        public MultiplePaymentViewController()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            (this.View.Model as IModelListViewWeb).InlineEditMode = InlineEditMode.Batch;
            this.View.Model.IsFooterVisible = true;
            this.View.Model.GroupSummary = "Sum(PaymentAmount)";
        }

        protected override void OnViewControlsCreated()
        {
            ListView listView = (ListView)this.View;

            if (!(listView.Editor is ASPxGridListEditor listEditor))
                return;


            if (!(listEditor.Grid is ASPxGridView gridView))
                return;

            listEditor.AllowEdit = true;

            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
