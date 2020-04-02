using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using MyWorkbench.BusinessObjects.Lookups;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;

namespace MyWorkbench.BusinessObjects.Common
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("Description")]
    [NavigationItem("Lookups")]
    public class Checklist : BaseObject
    {
        public Checklist(Session session)
            : base(session)
        {
        }

        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private ReportDataV2 fReport;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public ReportDataV2 Report {
            get {
                return fReport;
            }
            set {
                SetPropertyValue("Report", ref fReport, value);
            }
        }

        private string fNotes;
        [Size(-1)]
        public string Notes {
            get {
                return fNotes;
            }
            set {
                SetPropertyValue("Notes", ref fNotes, value);
            }
        }

        private WorkFlowType fWorkFlowType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public WorkFlowType WorkFlowType {
            get {
                return fWorkFlowType;
            }
            set {
                SetPropertyValue("WorkFlowType", ref fWorkFlowType, value);
            }
        }

        private WorkflowType fType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [Association("WorkflowType_Checklist")]
        [RuleRequiredField(DefaultContexts.Save)]
        public WorkflowType Type {
            get {
                return fType;
            }
            set {
                SetPropertyValue("Type", ref fType, value);
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("Checklist-ChecklistItems"), Aggregated]
        [RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<ChecklistItem> ChecklistItems {
            get {
                return GetCollection<ChecklistItem>("ChecklistItems");
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("WorkFlowChecklist_Checklist")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<WorkFlowChecklist> WorkFlowChecklists {
            get {
                return GetCollection<WorkFlowChecklist>("WorkFlowChecklists");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.Report = this.Session.FindObject<ReportDataV2>(CriteriaOperator.Parse("DisplayName == ?", "Job Card Check List"));
        }
    }
}