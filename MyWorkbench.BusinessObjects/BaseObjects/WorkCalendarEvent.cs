using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Delete;EditSeries;OpenSeries;RestoreOccurrence", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class WorkCalendarEvent : Event, IComparable, IWorkCalendarEvent, IWorkflowDesign
    {
        public WorkCalendarEvent(Session session)
            : base(session) {
        }

        #region Properties
        private EmployeeType fEmployeeType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public EmployeeType EmployeeType {
            get => fEmployeeType;
            set => SetPropertyValue(nameof(EmployeeType), ref fEmployeeType, value);
        }

        private Priority fPriority;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public Priority Priority {
            get => fPriority;
            set => SetPropertyValue(nameof(Priority), ref fPriority, value);
        }

        private DateTime? fCalenderSyncProcessed;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime? CalenderSyncProcessed {
            get => fCalenderSyncProcessed;
            set => SetPropertyValue(nameof(CalenderSyncProcessed), ref fCalenderSyncProcessed, value);
        }
        #endregion

        #region Links
        Project fProject = null;
        [EditorAlias(EditorAliases.ObjectPropertyEditor)]
        public Project Project {
            get { return fProject; }
            set {
                if (fProject == value)
                    return;

                Project prevfProject = fProject;
                fProject = value;

                if (IsLoading) return;

                if (prevfProject != null && prevfProject.WorkCalendarEvent == this)
                    prevfProject.WorkCalendarEvent = null;

                if (fProject != null)
                    fProject.WorkCalendarEvent = this;

                OnChanged("Project");
            }
        }

        JobCard fJobCard = null;
        [EditorAlias(EditorAliases.ObjectPropertyEditor)]
        public JobCard JobCard {
            get { return fJobCard; }
            set {
                if (fJobCard == value)
                    return;

                JobCard prevfJobCard = fJobCard;
                fJobCard = value;

                if (IsLoading) return;

                if (prevfJobCard != null && prevfJobCard.WorkCalendarEvent == this)
                    prevfJobCard.WorkCalendarEvent = null;

                if (fJobCard != null)
                    fJobCard.WorkCalendarEvent = this;

                OnChanged("JobCard");
            }
        }

        WorkFlowTask fWorkFlowTask = null;
        [EditorAlias(EditorAliases.ObjectPropertyEditor)]
        public WorkFlowTask WorkFlowTask {
            get { return fWorkFlowTask; }
            set {
                if (fWorkFlowTask == value)
                    return;

                WorkFlowTask prevWorkFlowTask = fWorkFlowTask;
                fWorkFlowTask = value;

                if (IsLoading) return;

                if (prevWorkFlowTask != null && prevWorkFlowTask.WorkCalendarEvent == this)
                    prevWorkFlowTask.WorkCalendarEvent = null;

                if (fWorkFlowTask != null)
                    fWorkFlowTask.WorkCalendarEvent = this;

                OnChanged("WorkFlowTask");
            }
        }

        Task fTask = null;
        [EditorAlias(EditorAliases.ObjectPropertyEditor)]
        public Task Task {
            get { return fTask; }
            set {
                if (fTask == value)
                    return;

                Task prevTask = fTask;
                fTask = value;

                if (IsLoading) return;

                if (prevTask != null && prevTask.WorkCalendarEvent == this)
                    prevTask.WorkCalendarEvent = null;

                if (fTask != null)
                    fTask.WorkCalendarEvent = this;

                OnChanged("Task");
            }
        }

        Booking fBooking = null;
        [EditorAlias(EditorAliases.ObjectPropertyEditor)]
        public Booking Booking {
            get { return fBooking; }
            set {
                if (fBooking == value)
                    return;

                Booking prevBooking = fBooking;
                fBooking = value;

                if (IsLoading) return;

                if (prevBooking != null && prevBooking.WorkCalendarEvent == this)
                    prevBooking.WorkCalendarEvent = null;

                if (fBooking != null)
                    fBooking.WorkCalendarEvent = this;

                OnChanged("Booking");
            }
        }
        #endregion

        protected override void OnSaving()
        {
            WorkFlowHelper.CreateWorkFlowProcess(this);

            base.OnSaving();
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<WorkCalendarEvent>().Execute(this);

            base.OnSaved();
        }
    }
}
