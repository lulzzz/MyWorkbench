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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "*", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class WorkFlowTime : BaseObject, IWorkFlowTimeTracking<WorkflowBase>, IModal
    {
        public WorkFlowTime(Session session)
            : base(session) {
        }

        #region Properties
        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowTime")]
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
