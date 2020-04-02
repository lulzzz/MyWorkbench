using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface.Attributes;
using System;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowTimeTrackingMultipleItem : BaseObject {
        public WorkFlowTimeTrackingMultipleItem(Session session)
            : base(session) {
        }

        #region Properties
        private WorkFlowTimeTrackingMultiple fWorkFlowTimeTrackingMultipleWorkflow;
        [Association("WorkFlowTimeTrackingMultiple_WorkFlowTimeTrackingMultipleItem")]
        public WorkFlowTimeTrackingMultiple WorkFlowTimeTrackingMultiple {
            get { return fWorkFlowTimeTrackingMultipleWorkflow; }
            set {
                SetPropertyValue("WorkFlowTimeTrackingMultiple", ref fWorkFlowTimeTrackingMultipleWorkflow, value);
            }
        }
        [RuleRequiredField(DefaultContexts.Save)]
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

        private DateTime fStartDateTime;
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        [EditorAlias("CustomDateTimeEditor")]
        [ImmediatePostData(true)]
        public DateTime StartDateTime {
            get => fStartDateTime;
            set => SetPropertyValue(nameof(StartDateTime), ref fStartDateTime, value);
        }

        private DateTime fEndDateTime;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        [EditorAlias("CustomDateTimeEditor")]
        public DateTime EndDateTime {
            get => fEndDateTime;
            set => SetPropertyValue(nameof(EndDateTime), ref fEndDateTime, value);
        }
        #endregion

        #region Events
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        #endregion
    }
}
