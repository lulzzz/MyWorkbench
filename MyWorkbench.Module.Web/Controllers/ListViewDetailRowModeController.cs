using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.Editors;
//using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web.SystemModule;
using Ignyt.BusinessInterface;

namespace MyWorkbench.Module.Web.Controllers {
    public partial class ListViewDetailRowModeController : ViewController<DevExpress.ExpressApp.ListView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            if (this.View.Model is IModelListViewWeb modelListView)
            {
                if (typeof(IDetailRowMode).IsAssignableFrom(this.View.ObjectTypeInfo.Type))
                {
                    modelListView.DetailRowMode = DetailRowMode.DetailViewWithActions;
                    //modelListView.DetailRowView.AllowEdit = true;
                }
                else
                    modelListView.DetailRowMode = DetailRowMode.None;
            }
        }
    }

    //public partial class DetailViewDetailRowModeController : ViewController<DevExpress.ExpressApp.DetailView>
    //{
    //    protected override void OnActivated()
    //    {
    //        base.OnActivated();
            
    //        if (!View.IsRoot & View.ViewEditMode == ViewEditMode.View)
    //        {
    //            var saveAction = new SimpleAction(this, "Save Changes", null)
    //            {
    //                SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
    //            };
    //            saveAction.Execute += saveAction_Execute;
    //            saveAction.SetClientScript("window.top.xaf.ConfirmUnsavedChangedController.DropModified()");
                
    //            View.ViewEditMode = ViewEditMode.Edit;
    //        }
    //    }

        //private void saveAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{
        //    this.ObjectSpace.CommitChanges();
        //}

        //protected override void OnViewControlsCreated()
        //{
        //    base.OnViewControlsCreated();

        //    if (!View.IsRoot)
        //    {
        //        if (Frame != null && Frame.Template is ISupportActionsToolbarVisibility)
        //        {
        //            ((ISupportActionsToolbarVisibility)Frame.Template).SetVisible(true);
        //        }
        //    }
        //}
    //}
}
