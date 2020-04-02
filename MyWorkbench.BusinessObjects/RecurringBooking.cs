using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.WorkFlow.Enum;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using MyWorkbench.BaseObjects.Constants;

namespace MyWorkbench.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Work Flow")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class RecurringBooking : WorkflowBase, IRecurringWorkFlow, IDetailRowMode
    {
        public RecurringBooking(Session session)
            : base(session)
        {
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.RecurringBooking;
            }
        }

        private Interval fIntervalPeriod;
        [DevExpress.Xpo.DisplayName("Interval")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public Interval IntervalPeriod {
            get { return fIntervalPeriod; }
            set { SetPropertyValue<Interval>("IntervalPeriod", ref fIntervalPeriod, value); }
        }

        private DateTime fStarting;
        [EditorAlias("CustomDateTimeEditor")]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, "@CurrentDate")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime Starting {
            get { return fStarting; }
            set { SetPropertyValue<DateTime>("Starting", ref fStarting, value); }
        }

        private DateTime? fNextDate;
        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Next Date")]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime? NextDate {
            get { return fNextDate; }
            set { SetPropertyValue<DateTime?>("NextDate", ref fNextDate, value); }
        }

        private WorkFlowStatus fStatus;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DevExpress.Xpo.DisplayName("Status")]
        public WorkFlowStatus WorkFlowStatus {
            get { return fStatus; }
            set { SetPropertyValue<WorkFlowStatus>("WorkFlowStatus", ref fStatus, value); }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return null;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return null;
            }
        }

        public void AdvanceNextDate()
        {
            switch (IntervalPeriod)
            {
                case Interval.Weekly:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddWeeks(1) : this.Starting.AddWeeks(1);
                    break;
                case Interval.Fortnightly:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddDays(14) : this.Starting.AddDays(14);
                    break;
                case Interval.Monthly:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddMonths(1) : this.Starting.AddMonths(1);
                    break;
                case Interval.Annually:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddYears(1) : this.Starting.AddYears(1);
                    break;
                case Interval.BiAnnually:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddMonths(6) : this.Starting.AddMonths(6);
                    break;
                case Interval.Daily:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddDays(1) : this.Starting.AddDays(1);
                    break;
                case Interval.Quarterly:
                    this.NextDate = this.NextDate != null ? ((DateTime)this.NextDate).AddMonths(3) : this.Starting.AddMonths(3);
                    break;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime CurrentDateTime {
            get {
                return Constants.DateTimeTimeZone(this.Session);
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "WorkFlowStatus")
                if (oldValue != newValue & newValue != null)
                {
                    this.Starting = Constants.DateTimeTimeZone(this.Session);
                }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
                this.Starting = Constants.DateTimeTimeZone(this.Session);
        }
    }
}
