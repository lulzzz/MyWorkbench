using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowTimeTrackingMultiple : BaseObject {
        public WorkFlowTimeTrackingMultiple(Session session)
            : base(session) {
        }

        #region Properties
        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowTimeTrackingMultiple")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [EditorAlias("CustomTokenCollectionEditor")]
        [Association("Employee_WorkFlowTimeTrackingMultiple")]
        public XPCollection<Employee> Employee {
            get {
                return GetCollection<Employee>("Employee");
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

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(true),VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
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
        #endregion

        #region Collections
        [Association("WorkFlowTimeTrackingMultiple_WorkFlowTimeTrackingMultipleItem"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public XPCollection<WorkFlowTimeTrackingMultipleItem> WorkFlowTimeTrackingMultipleItems {
            get {
                return GetCollection<WorkFlowTimeTrackingMultipleItem>("WorkFlowTimeTrackingMultipleItems");
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
            }
        }
        #endregion
    }
}
