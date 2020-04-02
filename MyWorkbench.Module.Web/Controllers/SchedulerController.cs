﻿using DevExpress.ExpressApp;
using System;
using Ignyt.BusinessInterface;
using DevExpress.Persistent.Base.General;
using DevExpress.Web.ASPxScheduler;
using DevExpress.ExpressApp.Scheduler.Web;
using System.Text;
using MyWorkbench.BusinessObjects.Lookups;
using MyWorkbench.Framework;
using Ignyt.BusinessInterface.Attributes;
using DevExpress.XtraScheduler;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using MyWorkbench.BusinessObjects.Helpers;
using DevExpress.XtraScheduler.Internal.Implementations;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using MyWorkbench.BusinessObjects.BaseObjects;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Actions;
using MyWorkbench.BusinessObjects;

namespace MyWorkbench.Module.Web.Controllers {
    public class SchedulerControllerResourceManager
    {
        public static IValueManager<List<object>> Resources;
        public static IValueManager<SchedulerViewType> ActiveViewType;
    }

    public class SchedulerListViewController : DevExpress.ExpressApp.Web.SystemModule.ListViewController
    {
        protected override void ExecuteEdit(DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs args)
        {
            if (View.CurrentObject.GetType() == typeof(WorkCalendarEvent))
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

    public class SchedulerWebNewObjectViewControllerController : WebNewObjectViewController
    {
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

    public class SchedulerController : ViewController<ListView>
    {
        private readonly System.ComponentModel.IContainer _components = null;

        private IObjectSpace _currentObjectSpace;
        private IObjectSpace CurrentObjectSpace {
            get {
                this._currentObjectSpace = this.View.ObjectSpace;
                return this._currentObjectSpace;
            }
        }

        private Session _session;
        private Session Session {
            get {
                this._session = ((XPObjectSpace)CurrentObjectSpace).Session;
                return this._session;
            }
        }

        public SchedulerController() {
            TargetObjectType = typeof(IEvent);
            _components = new System.ComponentModel.Container();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.View.ControlsCreated += View_ControlsCreated;

            if (SchedulerControllerResourceManager.Resources == null)
            {
                SchedulerControllerResourceManager.Resources = ValueManager.GetValueManager<List<object>>("SchedulerControllerResourceManager");

                SchedulerControllerResourceManager.Resources.Value = new List<object>();
            }

            if (SchedulerControllerResourceManager.ActiveViewType == null)
            {
                SchedulerControllerResourceManager.ActiveViewType = ValueManager.GetValueManager<SchedulerViewType>("SchedulerControllerSchedulerViewType");

                SchedulerControllerResourceManager.ActiveViewType.Value = DevExpress.XtraScheduler.SchedulerViewType.Agenda;
            }
        }

        private void View_ControlsCreated(object sender, EventArgs e) {
            if (this.View != null)
            {
                ASPxSchedulerListEditor listEditor = (ASPxSchedulerListEditor)this.View.Editor;
                ASPxScheduler scheduler = listEditor.SchedulerControl;

                #region Default Scheduler options
                ((ASPxDateNavigator)listEditor.DateNavigator).Visible = false;
                scheduler.ActiveViewType = SchedulerControllerResourceManager.ActiveViewType.Value;

                scheduler.EnableCallbackAnimation = true;
                scheduler.OptionsBehavior.ShowDetailedErrorInfo = true;
                scheduler.OptionsBehavior.ShowFloatingActionButton = true;
                scheduler.OptionsBehavior.ShowViewNavigator = true;
                scheduler.OptionsBehavior.ShowViewVisibleInterval = true;
                scheduler.OptionsToolTips.ShowAppointmentDragToolTip = true;
                scheduler.OptionsToolTips.ShowSelectionToolTip = true;
                scheduler.OptionsAdaptivity.Enabled = true;
                scheduler.OptionsForms.AppointmentFormVisibility = SchedulerFormVisibility.PopupWindow;
                scheduler.OptionsCustomization.AllowDisplayAppointmentFlyout = true;
                scheduler.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.All;
                scheduler.OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.All;
                scheduler.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.All;
                scheduler.OptionsBehavior.ShowRemindersForm = true;
                scheduler.OptionsToolTips.AppointmentToolTipMode = AppointmentToolTipMode.Auto;
                scheduler.OptionsBehavior.RemindersFormDefaultAction = DevExpress.XtraScheduler.RemindersFormDefaultAction.SnoozeAll;
                scheduler.InitAppointmentDisplayText += Scheduler_InitAppointmentDisplayText;
                scheduler.OptionsCookies.Enabled = true;
                scheduler.Storage.Appointments.ResourceSharing = true;
                scheduler.ResourceNavigator.Mode = ResourceNavigatorMode.Tokens;
                scheduler.ResourceNavigator.SettingsTokens.ShowResourceColor = true;
                scheduler.ResourceNavigator.SettingsTokens.ShowExpandButton = true;
                scheduler.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
                scheduler.ResourceNavigator.Visibility = ResourceNavigatorVisibility.Always;
                scheduler.Start = MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(this.Session).AddDays(-7);
                scheduler.Unload += Scheduler_Unload;
                scheduler.DataBound += Scheduler_DataBound;
                scheduler.PopupMenuShowing += Scheduler_PopupMenuShowing;
                scheduler.AppointmentsChanged += Scheduler_AppointmentsChanged;
                scheduler.ActiveViewChanging += Scheduler_ActiveViewChanging;

                if (scheduler != null)
                {
                    scheduler.Storage.Appointments.Statuses.Clear();
                    foreach (Priority priority in Enum.GetValues(typeof(Priority)))
                        scheduler.Storage.Appointments.Statuses.Add(System.Drawing.ColorTranslator.FromHtml(Attributes.GetAttribute<Color>(priority).ToString()), priority.ToString());

                    scheduler.Storage.Appointments.Labels.Clear();
                    foreach (Status status in this.View.ObjectSpace.GetObjects<Status>(null))
                        scheduler.Storage.Appointments.Labels.Add(status.UniqueID, status.Description, status.Description, status.Color);

                }
                #endregion

                #region Agenda View Options
                scheduler.AgendaView.AllowFixedDayHeaders = true;
                scheduler.AgendaView.DayCount = 70;
                scheduler.AgendaView.DayHeaderOrientation = AgendaDayHeaderOrientation.Vertical;
                scheduler.AgendaView.AppointmentDisplayOptions.ShowLabel = true;
                scheduler.AgendaView.AppointmentDisplayOptions.ShowResource = true;
                scheduler.AgendaView.AppointmentDisplayOptions.ShowTime = true;
                scheduler.AgendaView.AppointmentDisplayOptions.StatusDisplayType = AppointmentStatusDisplayType.Bounds;
                scheduler.AgendaView.Styles.ScrollAreaHeight = 600;
                scheduler.AgendaView.GroupType = SchedulerGroupType.Resource;
                #endregion

                #region Agenda View Options
                scheduler.TimelineView.IntervalCount = 70;
                scheduler.TimelineView.DisplayedIntervalCount = 5;
                scheduler.TimelineView.Styles.ScrollAreaHeight = 600;
                scheduler.TimelineView.GroupType = SchedulerGroupType.Resource;
                #endregion

            }
        }

        private void Scheduler_ActiveViewChanging(object sender, ActiveViewChangingEventArgs e)
        {
            ASPxSchedulerListEditor listEditor = (ASPxSchedulerListEditor)this.View.Editor;
            ASPxScheduler scheduler = listEditor.SchedulerControl;

            SchedulerControllerResourceManager.ActiveViewType.Value = e.NewView.Type;
            
            scheduler.DataBind();
        }

        private void Scheduler_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            List<object> resources = new List<object>();

            foreach (object obj in e.Objects)
            {
                foreach (object resource in (obj as AppointmentInstance).ResourceIds)
                {
                    resources.Add(resource);
                }
                
                EventEntryHelper.Execute(this.Session, Guid.Parse((obj as AppointmentInstance).Id.ToString()), (obj as AppointmentInstance).Start, 
                    (obj as AppointmentInstance).End, (obj as AppointmentInstance).LabelKey, (obj as AppointmentInstance).StatusKey, resources);

                CurrentObjectSpace.Refresh();
            }
        }

        private void Scheduler_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            #region Set Context Menu
            foreach (DevExpress.Web.MenuItem items in e.Menu.Items)
            {
                items.Visible = false;
            }

            DevExpress.Web.MenuItem item = e.Menu.Items.FindByName("StatusSubMenu");

            if (item != null)
            {
                item.Visible = true;
                item.Text = "Priority";
            }

            item = e.Menu.Items.FindByName("Xaf_Edit");

            if (item != null)
            {
                item.Visible = true;
                item.Text = "Edit";
            }

            item = e.Menu.Items.FindByName("LabelSubMenu");

            if (item != null)
            {
                item.Visible = true;
                item.Text = "Status";
            }
            #endregion
        }

        private void Scheduler_DataBound(object sender, EventArgs e)
        {
            ASPxSchedulerListEditor listEditor = (ASPxSchedulerListEditor)this.View.Editor;
            ASPxScheduler scheduler = listEditor.SchedulerControl;
            List<object> value = SchedulerControllerResourceManager.Resources.Value;

            if (SchedulerControllerResourceManager.ActiveViewType.Value != SchedulerViewType.Agenda)
            {
                if (value.Count == 0 && scheduler.Storage.Resources.Items.Count >= 1)
                {
                    WorkflowResource workflowResource = this.CurrentObjectSpace.GetObjects<WorkflowResource>(CriteriaOperator.Parse("ObjectOid == ?", SecuritySystem.CurrentUserId.ToString())).FirstOrDefault();

                    for (int i = 0; i < scheduler.Storage.Resources.Items.Count; i++)
                    {
                        DevExpress.XtraScheduler.Resource res = scheduler.Storage.Resources.Items[i];

                        if (res.Caption.Contains(workflowResource.Description))
                        {
                            value.Add(res.Id);
                            break;
                        }
                    }
                }

                scheduler.SetVisibleResources(value);
            }
            else
            {
                value = new List<object>();

                for (int i = 0; i < scheduler.Storage.Resources.Items.Count; i++)
                {
                    DevExpress.XtraScheduler.Resource res = scheduler.Storage.Resources.Items[i];

                    value.Add(res.Id);
                }

                scheduler.SetVisibleResources(value);
            }
        }

        private void Scheduler_Unload(object sender, EventArgs e)
        {
            ASPxScheduler scheduler = (ASPxScheduler)sender;

            if (SchedulerControllerResourceManager.ActiveViewType.Value != SchedulerViewType.Agenda)
            {
                ResourceBaseCollection resources = scheduler.ActiveView.GetResources();
                SchedulerControllerResourceManager.Resources.Value = resources.Select(r => r.Id).ToList();
            }
        }

        private void Scheduler_InitAppointmentDisplayText(object sender, DevExpress.XtraScheduler.AppointmentDisplayTextEventArgs e) {
            StringBuilder builder = new StringBuilder();

            builder.Append(e.Appointment.Subject);
            builder.Append(Environment.NewLine);
            builder.Append(e.Appointment.Location);
            builder.Append(Environment.NewLine);

            e.Text = builder.ToString();
        }

        protected override void OnDeactivated() {
            View.ControlsCreated += View_ControlsCreated;
            base.OnDeactivated();
        }

        protected override void Dispose(bool disposing) {
            if (disposing && (_components != null)) {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}