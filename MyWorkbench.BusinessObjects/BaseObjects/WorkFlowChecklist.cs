using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Data.Filtering;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Common;
using MyWorkbench.BusinessObjects.Lookups;
using System.Collections.Generic;
using Ignyt.Framework.ExpressApp;
using System.Linq;

namespace MyWorkbench.BusinessObjects.BaseObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    public class WorkFlowChecklist : BaseObject, IWorkFlowChecklist<WorkflowBase>, IStatusPriority<Status, Priority>, IModal
    {
        public WorkFlowChecklist(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowChecklist")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private Checklist fChecklist;
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("WorkFlowChecklist_Checklist")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [DataSourceProperty("CheckLists")]
        [ImmediatePostData]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public Checklist Checklist {
            get {
                return fChecklist;
            }
            set {
                SetPropertyValue("CheckList", ref fChecklist, value);
            }
        }

        private IList<Checklist> CheckLists {
            get {
                IList<Checklist> checkLists = null; 

                if (this.Workflow.Type != null)
                    checkLists = new XpoHelper(this.Session).GetObjects<Checklist>(CriteriaOperator.Parse("WorkFlowType == ? and Type == ?", (int)this.Workflow.WorkFlowType, this.Workflow.Type.Oid)).ToList();

                return checkLists;
            }
        }

        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("Description", Enabled = false, Criteria = "Checklist == null", Context = "DetailView")]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private Status fStatus;
        [DevExpress.Xpo.DisplayName("Status")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public Status Status {
            get {
                return fStatus;
            }
            set {
                SetPropertyValue("Status", ref fStatus, value);
            }
        }

        private Priority fPriority;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public Priority Priority {
            get {
                return fPriority;
            }
            set {
                SetPropertyValue("Priority", ref fPriority, value);
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("WorkFlowChecklist_WorkFlowChecklistItem")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("WorkFlowChecklistItems", Enabled = false, Criteria = "Checklist == null", Context = "DetailView")]
        [DevExpress.Xpo.DisplayName("Items")]
        public XPCollection<WorkFlowChecklistItem> WorkFlowChecklistItems {
            get {
                return GetCollection<WorkFlowChecklistItem>("WorkFlowChecklistItems");
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            this.Priority = Priority.Normal;
            this.Status = this.Session.FindObject<Status>(new BinaryOperator("IsDefault", 1, BinaryOperatorType.Equal));

            if (this.Status == null)
                this.Status = this.Session.FindObject<Status>(null);
        }
    }
}