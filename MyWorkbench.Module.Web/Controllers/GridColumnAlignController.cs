using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using MyWorkbench.Module.Web.Editors;
using DevExpress.ExpressApp.Editors;

namespace MyWorkbench.Module.Web.Controllers {
    public class GridColumnAlignController : ViewController<ListView>
    {
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View.Editor is ASPxGridListEditor gridListEditor)
            {
                foreach (ColumnWrapper wrapper in gridListEditor.Columns)
                {
                    IModelColumn columnModel = View.Model.Columns[wrapper.PropertyName];
                    if (columnModel != null && columnModel.PropertyEditorType == typeof(WebProgressPropertyEditor))
                    {
                        ((ASPxGridViewColumnWrapper)wrapper).Column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    }
                }
            }
        }
    }
}
