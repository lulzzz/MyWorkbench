using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    public enum EquipmentStatus {
        [ImageName("State_Validation_Valid")]
        New = 0,
        [ImageName("State_Validation_Skipped")]
        InProgress = 1,
        [ImageName("State_Validation_Warning")]
        Completed = 2,
        [ImageName("State_Validation_Invalid")]
        Scrapped = 3
    }

    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowEquipment : BaseObject, IWorkflowEquipment<WorkflowBase>, IModal, IWorkflowDesign
    {
        public WorkFlowEquipment(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowEquipment")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private Equipment fEquipment;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [Association("Equipment_WorkFlowEquipment")]
        public Equipment Equipment {
            get {
                return fEquipment;
            }
            set {
                SetPropertyValue("Equipment", ref fEquipment, value);
            }
        }

        private FaultType fFaultType;
        [Association("FaultType_WorkFlowEquipment")]
        public FaultType FaultType {
            get {
                return fFaultType;
            }
            set {
                SetPropertyValue("FaultType", ref fFaultType, value);
            }
        }

        private string fFaultDescription;
        [Size(1000)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string FaultDescription {
            get {
                return fFaultDescription;
            }
            set {
                SetPropertyValue("FaultDescription", ref fFaultDescription, value);
            }
        }

        private string fCode;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string Code {
            get {
                return fCode;
            }
            set {
                SetPropertyValue("Code", ref fCode, value);
            }
        }

        private string fRepairsDone;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Size(1000)]
        public string RepairsDone {
            get {
                return fRepairsDone;
            }
            set {
                SetPropertyValue("RepairsDone", ref fRepairsDone, value);
            }
        }

        private DateTime fDateBookedIn;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [DevExpress.Xpo.DisplayName("Booked In")]
        public DateTime DateBookedIn {
            get {
                return fDateBookedIn;
            }
            set {
                SetPropertyValue("DateBookedIn", ref fDateBookedIn, value);
            }
        }

        private DateTime fDateReturned;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [DevExpress.Xpo.DisplayName("Returned")]
        public DateTime DateReturned {
            get {
                return fDateReturned;
            }
            set {
                SetPropertyValue("DateReturned", ref fDateReturned, value);
            }
        }

        private EquipmentStatus fStatus;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public EquipmentStatus Status {
            get {
                return fStatus;
            }
            set {
                SetPropertyValue("Status", ref fStatus, value);
            }
        }

        private double fHoursRun;
        [VisibleInDetailView(true), VisibleInListView(false)]
        public double HoursRun {
            get {
                return fHoursRun;
            }
            set {
                SetPropertyValue("HoursRun", ref fHoursRun, value);
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
            this.DateBookedIn = Constants.DateTimeTimeZone(this.Session);
        }

        protected override void OnSaving()
        {
            WorkFlowHelper.CreateWorkFlowProcess(this);

            base.OnSaving();
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<WorkFlowEquipment>().Execute(this);

            base.OnSaved();
        }
    }
}
