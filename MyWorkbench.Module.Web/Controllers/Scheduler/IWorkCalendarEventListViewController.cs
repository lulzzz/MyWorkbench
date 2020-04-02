using DevExpress.ExpressApp;
using System;
using Ignyt.BusinessInterface;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using MyWorkbench.BusinessObjects.BaseObjects;
using DevExpress.Persistent.BaseImpl;

namespace MyWorkbench.Module.Web.Controllers.Scheduler
{
    public class IWorkCalendarEventListViewController : DevExpress.ExpressApp.Web.SystemModule.ListViewController
    {
        ////protected override void OnActivated()
        ////{
        ////    this.TargetObjectType = typeof(IWorkCalendarEvent);
        ////    base.OnActivated();
        ////}

        protected override void ExecuteEdit(DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs args)
        {
            if (typeof(IWorkCalendarEvent).IsAssignableFrom(View.CurrentObject.GetType()))
            {
                WorkCalendarEvent workCalendarEvent = this.View.CurrentObject as WorkCalendarEvent;

                if (workCalendarEvent.JobCard != null)
                    this.LoadDetailView(args, workCalendarEvent.JobCard);
                else if (workCalendarEvent.Project != null)
                    this.LoadDetailView(args, workCalendarEvent.Project);
                else if (workCalendarEvent.WorkFlowTask != null)
                    this.LoadDetailView(args, workCalendarEvent.WorkFlowTask);
                else if (workCalendarEvent.Task != null)
                    this.LoadDetailView(args, workCalendarEvent.Task);
                else if (workCalendarEvent.Booking != null)
                    this.LoadDetailView(args, workCalendarEvent.Booking);

                return;
            }

            base.ExecuteEdit(args);
        }

        private void LoadDetailView(DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs args, object Workflow)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            Session session = ((XPObjectSpace)objectSpace).Session;
            object currentObject = session.FindObject(Workflow.GetType(), CriteriaOperator.Parse("Oid == ?", (Workflow as BaseObject).Oid));

            DetailView view = Application.CreateDetailView(objectSpace, currentObject, true);
            view.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            view.Closed += View_Closed;

            args.ShowViewParameters.CreatedView = view;
            args.ShowViewParameters.CreatedView.Tag = objectSpace.GetKeyValue(currentObject);
            args.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            args.ShowViewParameters.Context = TemplateContext.PopupWindow;
        }

        private void View_Closed(object sender, EventArgs e)
        {
            this.View.ObjectSpace.Refresh();
        }
    }
}
