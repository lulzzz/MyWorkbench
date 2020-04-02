using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface.Attributes;
using System;
using MyWorkbench.BaseObjects.Constants;

namespace MyWorkbench.BusinessObjects.WorkFlow
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    public class WorkFlowProcess : BaseObject
    {
        public WorkFlowProcess(Session session)
            : base(session)
        {
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("WorkFlow_WorkFlowProcess")]
        public XPCollection<WorkFlow> WorkFlow {
            get {
                return GetCollection<WorkFlow>("WorkFlow");
            }
        }

        private DateTime fDateReceived;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime DateReceived {
            get {
                return fDateReceived;
            }
            set {
                SetPropertyValue("DateReceived", ref fDateReceived, value);
            }
        }

        private DateTime fDateProcessed;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime DateProcessed {
            get {
                return fDateProcessed;
            }
            set {
                SetPropertyValue("DateProcessed", ref fDateProcessed, value);
            }
        }

        private Nullable<DateTime> fDateToExecute;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public Nullable<DateTime> DateToExecute {
            get {
                return fDateToExecute;
            }
            set {
                SetPropertyValue("DateToExecute", ref fDateToExecute, value);
            }
        }

        private string fType;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public string Type {
            get {
                return fType;
            }
            set {
                SetPropertyValue("Type", ref fType, value);
            }
        }

        private string fCriterion;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public string Criterion {
            get {
                return fCriterion;
            }
            set {
                SetPropertyValue("Criterion", ref fCriterion, value);
            }
        }

        private bool fIsNewObject;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public bool IsNewObject {
            get {
                return fIsNewObject;
            }
            set {
                SetPropertyValue("IsNewObject", ref fIsNewObject, value);
            }
        }

        private double fExecutions;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public double Executions {
            get {
                return fExecutions;
            }
            set {
                SetPropertyValue("Executions", ref fExecutions, value);
            }
        }

        private string fExecutedResult;
        [Size(-1)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public string ExecutedResult {
            get {
                return fExecutedResult;
            }
            set {
                SetPropertyValue("ExecutedResult", ref fExecutedResult, value);
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("WorkFlowProcess_WorkFlowProcessTracking")]
        public XPCollection<WorkFlowProcessTracking> WorkFlowProcessTracking {
            get {
                return GetCollection<WorkFlowProcessTracking>("WorkFlowProcessTracking");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.DateReceived = Constants.DateTimeTimeZone(this.Session);
        }
    }
}
