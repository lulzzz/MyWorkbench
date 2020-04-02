using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Web;
using Ignyt.BusinessInterface;
using System.Web.UI.WebControls;

namespace MyWorkbench.Module.Web.Controllers {
    public class ListViewEndlessPagingController : ViewController<DevExpress.ExpressApp.ListView> {
        public ListViewEndlessPagingController() {
            this.TargetObjectType = typeof(BaseObject);
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();

            if (MyWorkbench.BaseObjects.Constants.Constants.AllowEndlessPaging(((XPObjectSpace)this.ObjectSpace).Session)) {
                if (typeof(IEndlessPaging).IsAssignableFrom(this.View.ObjectTypeInfo.Type)) {
                    if (View.Editor is ASPxGridListEditor gridListEditor) {
                        gridListEditor.Grid.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
                        gridListEditor.Grid.SettingsPager.PageSize = 20;
                        gridListEditor.Grid.Width = Unit.Percentage(100);                        
                        gridListEditor.Grid.Settings.VerticalScrollableHeight = 600;


                        foreach (GridViewColumn column in gridListEditor.Grid.Columns) {
                            if (column is GridViewDataActionColumn) {
                                column.Width = 40;
                            }

                            if (column.Name.Contains("Selection")) {
                                column.Width = 50;
                            }
                        }
                    }
                } else {
                    if (View.Editor is ASPxGridListEditor gridListEditor) {
                        gridListEditor.Grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                        gridListEditor.Grid.Width = Unit.Percentage(100);
                    }
                }
            } else {
                if (View.Editor is ASPxGridListEditor gridListEditor) {
                    gridListEditor.Grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                    gridListEditor.Grid.Width = Unit.Percentage(100);
                }
            }
        }
    }
}
