using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("Action_Debug_Stop")]
    public class EmployeeRate : BaseObject {
        public EmployeeRate(Session session)
            : base(session) {
        }

        #region Properties
        private Employee fEmployeen;
        [Association("Employee_EmployeeRate")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Employee Employee {
            get => fEmployeen;
            set => SetPropertyValue(nameof(Employee), ref fEmployeen, value);
        }

        private double fRatePerHour;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("Index", "7")]
        public double RatePerHour {
            get => fRatePerHour;
            set => SetPropertyValue(nameof(RatePerHour), ref fRatePerHour, value);
        }

        private DateTime fDateTime;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime ValidFrom {
            get => fDateTime;
            set => SetPropertyValue(nameof(DateTime), ref fDateTime, value);
        }

        #endregion
    }
}
