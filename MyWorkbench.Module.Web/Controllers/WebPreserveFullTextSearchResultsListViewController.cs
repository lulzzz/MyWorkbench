using DevExpress.ExpressApp;
using System;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.SystemModule;

namespace MyWorkbench.Module.Web.Controllers {
    public class WebPreserveFullTextSearchResultsListViewController : ViewController<ListView>, IModelExtender
    {
        private FilterController filterController = null;

        protected override void OnActivated()
        {
            base.OnActivated();
            string parameter = ((IModelListViewFullTextSearch)View.Model).FullTextSearchParameter;
            filterController = Frame.GetController<FilterController>();
            if (filterController != null)
            {
                if (!String.IsNullOrEmpty(parameter))
                {
                    filterController.FullTextFilterAction.DoExecute(parameter);
                }
                filterController.FullTextFilterAction.Execute += FullTextFilterAction_Execute;
            }
        }

        protected override void OnDeactivated()
        {
            if (filterController != null)
            {
                filterController.FullTextFilterAction.Execute -= FullTextFilterAction_Execute;
            }
            base.OnDeactivated();
        }

        void FullTextFilterAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            ((IModelListViewFullTextSearch)View.Model).FullTextSearchParameter = (String)e.ParameterCurrentValue;
        }

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add(typeof(IModelListViewWeb), typeof(IModelListViewFullTextSearch));
        }
    }

    public interface IModelListViewFullTextSearch
    {
        string FullTextSearchParameter { get; set; }
    }
}
