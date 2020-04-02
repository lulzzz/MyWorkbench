using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework.ExpressApp;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowTimeTracking : BaseObject, IWorkFlowTimeTracking<WorkflowBase>, IModal
    {
        public WorkFlowTimeTracking(Session session)
            : base(session) {
        }

        #region Properties
        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowTimeTracking")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
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

        private double fLatitude;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Latitude {
            get => fLatitude;
            set => SetPropertyValue(nameof(Latitude), ref fLatitude, value);
        }

        private double fLongitude;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Longitude {
            get => fLongitude;
            set => SetPropertyValue(nameof(Longitude), ref fLongitude, value);
        }

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(true),VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Day")]
        public string DayName {
            get {
                if (this.StartDateTime != null && this.StartDateTime != DateTime.MinValue)
                    return this.StartDateTime.DayOfWeek.ToString();
                else
                    return null;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Week")]
        public string Week {
            get {
                return this.StartDateTime.WeekOfYear(Constants.PaymentDayOfWeek(this.Session));
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Year")]
        public string Year {
            get {
                return this.StartDateTime.YearOfWeek(Constants.PaymentDayOfWeek(this.Session));
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Month")]
        public string Month {
            get {
                return this.StartDateTime.ToMonthName();
            }
        }

        private DateTime fStartDateTime;
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        [ImmediatePostData(true)]
        public DateTime StartDateTime {
            get => fStartDateTime;
            set => SetPropertyValue(nameof(StartDateTime), ref fStartDateTime, value);
        }

        private DateTime fEndDateTime;
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime EndDateTime {
            get => fEndDateTime;
            set => SetPropertyValue(nameof(EndDateTime), ref fEndDateTime, value);
        }

        private Party fParty;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Created By")]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private Employee fEmployee;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public Employee Employee {
            get => fEmployee;
            set => SetPropertyValue(nameof(Employee), ref fEmployee, value);
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Hours")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double NumberOfHours {
            get {
                return (this.EndDateTime - this.StartDateTime).TotalHours;
            }
        }

        private bool fLunch;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public bool Lunch {
            get => fLunch;
            set => SetPropertyValue(nameof(Lunch), ref fLunch, value);
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Normal")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double NormalHours {
            get {
                double result = (double)this.EndDateTime.SplitMinutesToDays(this.StartDateTime).Where(g => g.DayOfWeek != DayOfWeek.Saturday & g.DayOfWeek != DayOfWeek.Sunday & g.PublicHoliday == false).Sum(g => g.Hours);

                if(result >= 1)
                    result -= (this.Lunch ? this.NumberOfLunch : 0);

                return result > 9 ? 9 : result;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Lunch")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double NumberOfLunch {
            get {
                return (this.Lunch ? 1 : 0);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Overtime")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double NumberOfOverTimeHours {
            get {
                double result = (double)this.EndDateTime.SplitMinutesToDays(this.StartDateTime).Where(g => g.DayOfWeek != DayOfWeek.Saturday & g.DayOfWeek != DayOfWeek.Sunday & g.PublicHoliday == false).Sum(g => g.Hours);

                if (result >= 1)
                    result -= this.NumberOfLunch;

                if (result > 9)
                    result -= 9;
                else
                    result = 0;

                result += (double)this.EndDateTime.SplitMinutesToDays(this.StartDateTime).Where(g => g.DayOfWeek == DayOfWeek.Saturday & g.PublicHoliday == false).Sum(g => g.Hours);

                if (this.NormalHours == 0 & result >= 1)
                    result -= this.NumberOfLunch;

                return result;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Doubletime")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double NumberOfDoubleTimeHours {
            get {
                double result = (double)this.EndDateTime.SplitMinutesToDays(this.StartDateTime).Where(g => g.DayOfWeek == DayOfWeek.Sunday | g.PublicHoliday == true).Sum(g => g.Hours);

                if (this.NormalHours == 0 & this.NumberOfOverTimeHours == 0 & result >= 1)
                    result -= this.NumberOfLunch;

                return result;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Total")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double NumberOfTotalHours {
            get {
                return (this.NormalHours + (this.NumberOfOverTimeHours * 1.5)  + (this.NumberOfDoubleTimeHours * 2));
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Current Rate")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double CurrentRate {
            get {
                EmployeeRate employeeRate = null;

                if (this.Employee != null)
                {
                    List<EmployeeRate> employeeRates = new XpoHelper(this.Session).GetObjects<EmployeeRate>(CriteriaOperator.Parse("Employee = ?", this.Employee.Oid)).ToList();
                    employeeRate = employeeRates.Where(g => g.ValidFrom <= this.StartDateTime).OrderByDescending(g => g.ValidFrom).FirstOrDefault();
                }

                if (employeeRate == null)
                    return 0;
                else
                    return employeeRate.RatePerHour;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Total Pay")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "N2")]
        public double TotalPay {
            get {
                return this.NumberOfTotalHours * this.CurrentRate;
            }
        }
        #endregion

        #region Events
        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.DateTime = Constants.DateTimeTimeZone(this.Session);
                this.Lunch = true;
            }
        }
        #endregion
    }
}
