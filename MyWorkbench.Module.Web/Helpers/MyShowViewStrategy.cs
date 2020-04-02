using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using Ignyt.BusinessInterface;

namespace MyWorkbench.Module.Web.Helpers
{
    public class MyShowViewStrategy : ShowViewStrategy
    {
        public MyShowViewStrategy(XafApplication application) : base(application) { }
        protected override void ShowViewCore(ShowViewParameters parameters, ShowViewSource showViewSource)
        {
            if (parameters.CreatedView is DetailView)
            {
                if (parameters.CreatedView.CurrentObject != null)
                {
                    if (typeof(IModal).IsAssignableFrom(parameters.CreatedView.CurrentObject.GetType()))
                        ShowViewInModalWindow(parameters, showViewSource);
                    else
                        base.ShowViewCore(parameters, showViewSource);
                }
                else {
                    base.ShowViewCore(parameters, showViewSource);
                }
            }
            else
            {
                base.ShowViewCore(parameters, showViewSource);
            }
        }
    }
}
