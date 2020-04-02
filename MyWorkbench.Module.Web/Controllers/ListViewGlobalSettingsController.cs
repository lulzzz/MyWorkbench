using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Web;

namespace MyWorkbench.Module.Web.Controllers
{
    public class ListViewGlobalSettingsController : ViewController<ListView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            if (this.View.Model is IModelListViewWeb modelListView)
            {
                modelListView.IsAdaptive = true;
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ASPxGridListEditor listEditor = ((ListView)View).Editor as ASPxGridListEditor;
            if (listEditor != null)
            {
                listEditor.Grid.CustomColumnDisplayText += new ASPxGridViewColumnDisplayTextEventHandler(Grid_CustomColumnDisplayText);
                listEditor.Grid.HtmlDataCellPrepared += new ASPxGridViewTableDataCellEventHandler(Grid_HtmlDataCellPrepared);
            }
        }
        void Grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.CellValue != null)
                e.Cell.ToolTip = e.CellValue.ToString();
        }

        void Grid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Value != null)
            {
                string cellValue = e.Value.ToString();

                if (cellValue.Length > 40)
                    e.DisplayText = cellValue.Substring(0, 40) + "...";
            }
        }
    }
}
