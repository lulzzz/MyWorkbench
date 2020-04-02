using DevExpress.ExpressApp;
using System;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Actions;
using MyWorkbench.BusinessObjects;

namespace MyWorkbench.Module.Web.Controllers.Scheduler
{
    public class IWorkCalendarEventWebNewObjectViewController : WebNewObjectViewController
    {
        protected override void OnActivated()
        {
            this.TargetObjectType = typeof(IWorkCalendarEvent);
            base.OnActivated();
        }

        protected override void New(SingleChoiceActionExecuteEventArgs args)
        {
            if (this.View.Id == "WorkCalendarEvent_JobCard_ListView")
                this.LoadDetailView(args, typeof(JobCard));
            else if (this.View.Id == "WorkCalendarEvent_Project_ListView")
                this.LoadDetailView(args, typeof(Project));
            else if (this.View.Id == "WorkCalendarEvent_Task_ListView")
                this.LoadDetailView(args, typeof(MyWorkbench.BusinessObjects.Task));
            else if (this.View.Id == "WorkCalendarEvent_Booking_ListView")
                this.LoadDetailView(args, typeof(Booking));
            else if (this.View.Id == "WorkCalendarEvent_WorkflowTask_ListView")
                this.LoadDetailView(args, typeof(WorkFlowTask));
            else
                base.New(args);
        }

        private void LoadDetailView(DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs args, Type Type)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            object currentObject = objectSpace.CreateObject(Type);

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
