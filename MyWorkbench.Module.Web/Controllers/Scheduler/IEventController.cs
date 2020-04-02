using DevExpress.ExpressApp;
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

namespace MyWorkbench.Module.Web.Controllers.Scheduler
{
    public class IEventController : DevExpress.ExpressApp.Web.SystemModule.ListViewController
    {
        private readonly System.ComponentModel.IContainer _components = null;
        private SchedulerViewType _schedulerViewType = DevExpress.XtraScheduler.SchedulerViewType.Agenda;

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

        public IEventController()
        {
            this.TargetObjectType = typeof(IEvent);
            _components = new System.ComponentModel.Container();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.View.ControlsCreated += View_ControlsCreated;

            if (IEventControllerResourceManager.Resources == null)
            {
                IEventControllerResourceManager.Resources = ValueManager.GetValueManager<List<object>>("SchedulerControllerResourceManager");

                IEventControllerResourceManager.Resources.Value = new List<object>();
            }
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            if (this.View != null)
            {
                ASPxSchedulerListEditor listEditor = (ASPxSchedulerListEditor)this.View.Editor;
                ASPxScheduler scheduler = listEditor.SchedulerControl;

                // Agenda View
                scheduler.AgendaView.AllowFixedDayHeaders = true;
                scheduler.AgendaView.DayCount = 30;
                scheduler.AgendaView.DayHeaderOrientation = AgendaDayHeaderOrientation.Vertical;
                scheduler.AgendaView.AppointmentDisplayOptions.ShowLabel = true;
                scheduler.AgendaView.AppointmentDisplayOptions.ShowResource = true;
                scheduler.AgendaView.GroupType = SchedulerGroupType.None;
                scheduler.AgendaView.Styles.ScrollAreaHeight = 450;

                // TimelineView
                scheduler.TimelineView.Styles.ScrollAreaHeight = 450;
                scheduler.TimelineView.IntervalCount = 30;
                scheduler.TimelineView.DisplayedIntervalCount = 7;

                // ASPxScheduler Global Settings
                ((ASPxDateNavigator)listEditor.DateNavigator).Visible = true;
                scheduler.ActiveViewType = this._schedulerViewType;
                scheduler.WorkWeekView.Enabled = false;
                scheduler.EnableCallbackAnimation = true;
                scheduler.OptionsBehavior.ShowDetailedErrorInfo = true;
                scheduler.OptionsBehavior.ShowFloatingActionButton = true;
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
                scheduler.Start = MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(this.Session);
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
            }
        }

        private void Scheduler_ActiveViewChanging(object sender, ActiveViewChangingEventArgs e)
        {
            ASPxSchedulerListEditor listEditor = (ASPxSchedulerListEditor)this.View.Editor;
            ASPxScheduler scheduler = listEditor.SchedulerControl;

            this._schedulerViewType = e.NewView.Type;
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
        }

        private void Scheduler_DataBound(object sender, EventArgs e)
        {
            ASPxSchedulerListEditor listEditor = (ASPxSchedulerListEditor)this.View.Editor;
            ASPxScheduler scheduler = listEditor.SchedulerControl;

            if (this._schedulerViewType != SchedulerViewType.Agenda)
            {
                List<object> value = IEventControllerResourceManager.Resources.Value;

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
        }

        private void Scheduler_Unload(object sender, EventArgs e)
        {
            ASPxScheduler scheduler = (ASPxScheduler)sender;
            ResourceBaseCollection resources = scheduler.ActiveView.GetResources();

            if (this._schedulerViewType != SchedulerViewType.Agenda)
            {
                IEventControllerResourceManager.Resources.Value = resources.Select(r => r.Id).ToList();
            }
        }

        private void Scheduler_InitAppointmentDisplayText(object sender, DevExpress.XtraScheduler.AppointmentDisplayTextEventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(e.Appointment.Subject);
            builder.Append(Environment.NewLine);
            builder.Append(e.Appointment.Location);
            builder.Append(Environment.NewLine);

            e.Text = builder.ToString();
        }

        protected override void OnDeactivated()
        {
            View.ControlsCreated += View_ControlsCreated;
            base.OnDeactivated();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
