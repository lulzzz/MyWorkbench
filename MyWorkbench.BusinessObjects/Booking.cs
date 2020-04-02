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
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Work Flow")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Booking : WorkflowBase, ICustomizable, IEventEntryObject<WorkCalendarEvent>, ITimeTracking, IDetailRowMode, IKanban
    {
        public Booking(Session session)
            : base(session)
        {
        }

        #region Properties
        private PaymentType fPaymentType;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public PaymentType PaymentType {
            get {
                return fPaymentType;
            }
            set {
                SetPropertyValue("PaymentType", ref fPaymentType, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.Booking;
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

                if (prevWorkCalendarEvent != null && prevWorkCalendarEvent.Booking == this)
                    prevWorkCalendarEvent.Booking = null;

                if (fWorkCalendarEvent != null)
                    fWorkCalendarEvent.Booking = this;

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
        protected override void OnSaving()
        {
            base.OnSaving();
        }

        protected override void OnSaved()
        {
            EventEntryHelper.Execute<Booking>(this);
            new WorkFlowExecute<Booking>().Execute(this);

            base.OnSaved();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                this.PaymentType = PaymentType.Cash;
            }
        }
        #endregion
    }
}
