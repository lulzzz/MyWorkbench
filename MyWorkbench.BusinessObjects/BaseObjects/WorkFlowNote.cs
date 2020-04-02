using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowNote : BaseObject, IWorkflowNote<WorkflowBase>, IModal
    {
        public WorkFlowNote(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowNote")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private WorkFlowTask fWorkFlowTask;
        [Association("WorkFlowTask_WorkFlowNote")]
        [VisibleInListView(false),VisibleInDetailView(false)]
        public WorkFlowTask WorkFlowTask {
            get { return fWorkFlowTask; }
            set {
                SetPropertyValue("WorkFlowTask", ref fWorkFlowTask, value);
            }
        }

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("RowCount", "10")]
        [VisibleInDetailView(true),VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private string fCreatedBy;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string CreatedBy {
            get => fCreatedBy;
            set => SetPropertyValue(nameof(CreatedBy), ref fCreatedBy, value);
        }

        private DateTime fDateTime;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(false), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime DateTime {
            get => fDateTime;
            set => SetPropertyValue(nameof(DateTime), ref fDateTime, value);
        }

        private Party fParty;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Created By")]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
            
            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);


                this.DateTime = Constants.DateTimeTimeZone(this.Session);
            }
        }
    }
}
