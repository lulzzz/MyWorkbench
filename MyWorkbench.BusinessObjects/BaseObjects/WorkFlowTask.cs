using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ignyt.BusinessInterface.Kanban;
using Ignyt.Framework.ExpressApp;

namespace MyWorkbench.BusinessObjects.BaseObjects
{
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowTask : BaseObject, IWorkflowTask<WorkflowBase>, IEventEntryObject<WorkCalendarEvent>, IModal, IStatusPriority<Status, Priority>, IEndlessPaging, IDetailRowMode, IWorkflowDesign
    {
        public WorkFlowTask(Session session)
            : base(session)
        {
        }

        #region Properties
        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowTask")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        [DevExpress.Xpo.DisplayName("Assigned To")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [EditorAlias("CustomTokenCollectionEditor")]
        [Association("WorkflowResource_WorkFlowTask")]
        public XPCollection<WorkflowResource> WorkflowResources {
            get {
                return GetCollection<WorkflowResource>("WorkflowResources");
            }
        }

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("RowCount", "3")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private DateTime fDateTime;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime DateTime {
            get => fDateTime;
            set => SetPropertyValue(nameof(Description), ref fDateTime, value);
        }

        private Party fParty;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Created By")]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private DateTime? fStartDate;
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Start")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime? BookedTime {
            get {
                return fStartDate;
            }
            set {
                SetPropertyValue("BookedTime", ref fStartDate, value);
            }
        }

        private DateTime? fEndDate;
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Finish")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime? BookedTimeEnd {
            get {
                return fEndDate;
            }
            set {
                SetPropertyValue("BookedTimeEnd", ref fEndDate, value);
            }
        }

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

        private Status fStatus;
        [DevExpress.Xpo.DisplayName("Status")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [NonCloneable]
        public Status Status {
            get {
                return fStatus;
            }
            set {
                SetPropertyValue("Status", ref fStatus, value);
            }
        }

        private Priority fPriority;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public Priority Priority {
            get {
                return fPriority;
            }
            set {
                SetPropertyValue("Priority", ref fPriority, value);
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

                if (prevWorkCalendarEvent != null && prevWorkCalendarEvent.WorkFlowTask == this)
                    prevWorkCalendarEvent.WorkFlowTask = null;

                if (fWorkCalendarEvent != null)
                    fWorkCalendarEvent.WorkFlowTask = this;

                OnChanged("WorkCalendarEvent");
            }
        }
        #endregion

        #region Collections
        [Association("WorkFlowTask_WorkFlowNote"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public XPCollection<WorkFlowNote> Notes {
            get {
                return GetCollection<WorkFlowNote>("Notes");
            }
        }

        [Association("WorkFlowTask_WorkFlowImage"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkFlowImage> Images {
            get {
                return GetCollection<WorkFlowImage>("Images");
            }
        }

        [Association("WorkFlowTask_WorkFlowAttachment"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkFlowAttachment> Attachments {
            get {
                return GetCollection<WorkFlowAttachment>("Attachments");
            }
        }
        #endregion

        #region Interfaces
        // IEventEntry
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string EventSubject {
            get {
                return this.Description;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string EventDescription {
            get {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", "Description: ", this.Description, ", Assigned To: ", this.AssignedTo, ", Status: ", this.Status.Description, ", Priority: ", this.Priority.ToString(), ", ETA: ", this.BookedTimeEnd.ToString());
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
                return null;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IStatus EventStatus {
            get {
                return this.Status;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string AssignedTo {
            get {
                string assignedTo = string.Empty;

                foreach (WorkflowResource resource in this.WorkflowResources)
                {
                    if (assignedTo == string.Empty)
                        assignedTo = resource.Caption;
                    else
                        assignedTo = string.Concat(assignedTo, " and ", resource.Caption);
                }

                return assignedTo;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IEnumerable<DevExpress.Persistent.Base.General.IResource> Resources {
            get {
                return this.WorkflowResources.ToList();
            }

        }
        #endregion

        #region Events
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "BookedTime")
            {
                if (oldValue != newValue & newValue != null)
                {
                    if (this.BookedTime != null)
                        this.BookedTimeEnd = ((DateTime)this.BookedTime).AddHours(Constants.AppointmentLength(this.Session));
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                this.DateTime = Constants.DateTimeTimeZone(this.Session);
                this.Status = this.Session.FindObject<Status>(new BinaryOperator("IsDefault", 1, BinaryOperatorType.Equal));

                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

            }
        }

        protected override void OnSaving()
        {
            if (this.Workflow != null && this.Workflow.WorkflowResources != null && this.Workflow.WorkflowResources.Count >= 1)
                this.WorkflowResources.AddRange(this.Workflow.WorkflowResources);

            WorkFlowHelper.CreateWorkFlowProcess(this);

            base.OnSaving();
        }

        protected override void OnSaved()
        {
            EventEntryHelper.Execute<WorkFlowTask>(this);

            new WorkFlowExecute<WorkFlowTask>().Execute(this);

            base.OnSaved();
        }
        #endregion
    }
}
