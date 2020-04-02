using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Kanban;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Work Flow")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Task : WorkflowBase, ICustomizable, IEventEntryObject<WorkCalendarEvent>, ITimeTracking, IDetailRowMode, IKanban
    {
        public Task(Session session)
            : base(session)
        {
        }

        #region Properties
        private float fProgress;
        [VisibleInListView(true), VisibleInDetailView(false)]
        [DevExpress.Xpo.DisplayName("Progress")]
        [EditorAlias("CustomProgressEditor")]
        public float Progress {
            get {
                return fProgress;
            }
            set {
                SetPropertyValue("Progress", ref fProgress, value);
            }
        }

        private float fProgressValue;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Progress Value")]
        [ModelDefault("MinValue", "0")]
        [ModelDefault("MaxValue", "100")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public float ProgressValue {
            get {
                return fProgressValue;
            }
            set {
                this.Progress = value;
                SetPropertyValue("ProgressValue", ref fProgressValue, value);
            }
        }

        [VisibleInListView(true), VisibleInDetailView(false)]
        [DevExpress.Xpo.DisplayName("Assigned Days")]
        public double DaysToComplete {
            get {
                if (this.BookedTime != null & this.BookedTimeEnd != null)
                    return Math.Round(((DateTime)this.BookedTimeEnd - (DateTime)this.BookedTime).TotalDays,2);
                else
                    return 0;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.Task;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return string.Empty;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return string.Empty;
            }
        }
        #endregion

        #region IEventEntryObject
        WorkCalendarEvent fWorkCalendarEvent = null;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public WorkCalendarEvent WorkCalendarEvent {
            get { return fWorkCalendarEvent; }
            set {
                if (fWorkCalendarEvent == value)
                    return;

                WorkCalendarEvent prevWorkCalendarEvent = fWorkCalendarEvent;
                fWorkCalendarEvent = value;

                if (IsLoading) return;

                if (prevWorkCalendarEvent != null && prevWorkCalendarEvent.Task == this)
                    prevWorkCalendarEvent.Task = null;

                if (fWorkCalendarEvent != null)
                    fWorkCalendarEvent.Task = this;

                OnChanged("WorkCalendarEvent");
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string EventSubject {
            get {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}", this.No, ": ", Type == null ? null : this.Type.Description, ", ", this.Vendor == null ? null : this.Vendor.FullName, ", ", this.ReferenceNumber);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string EventDescription {
            get {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", "Description: ", string.IsNullOrEmpty(this.Subject) == false ? this.Subject : this.Description, ", Assigned To: ", this.AssignedTo, ", Status: ", this.Status.Description, ", Priority: ", this.Priority.ToString(), ", ETA: ", this.BookedTimeEnd.ToString());
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string EventFullDescription {
            get {
                return string.Format("{0}{1}{3}4{4}{5}", "Subject: ", this.EventSubject, ", Description: ", this.EventDescription, ", Location: ", this.EventLocation);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string EventLocation {
            get {
                return Location == null ? null : this.Location.FullAddress;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IStatus EventStatus {
            get {
                return this.Status;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IEnumerable<DevExpress.Persistent.Base.General.IResource> Resources {
            get {
                return this.WorkflowResources.ToList();
            }
        }
        #endregion

        #region IKanban
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string IStatus {
            get {
                return this.Status.Description;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string IDescription {
            get {
                return this.FullDescription;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string ImageUrl {
            get {
                return null;
            }
        }
        #endregion

        #region Events
        protected override void OnSaved()
        {
            EventEntryHelper.Execute<Task>(this);
            new WorkFlowExecute<Task>().Execute(this);

            base.OnSaved();
        }
        #endregion

    }
}
