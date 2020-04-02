using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using System;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowSignature : BaseObject, IWorkflowSignature<WorkflowBase>, IModal, IWorkflowDesign
    {
        public WorkFlowSignature(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowSignature")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private System.Drawing.Image fImage;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 100, DetailViewImageEditorFixedHeight = 100)]
        public System.Drawing.Image Image {
            get { return fImage; }
            set {
                SetPropertyValue("Image", ref fImage, value);
            }
        }

        private DateTime fCreated;
        [VisibleInDetailView(false), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime Created {
            get {
                return fCreated;
            }
            set {
                SetPropertyValue("Created", ref fCreated, value);
            }
        }

        private double fLatitude;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Latitude {
            get {
                return fLatitude;
            }
            set {
                SetPropertyValue("Latitude", ref fLatitude, value);
            }
        }

        private double fLongitude;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Longitude {
            get {
                return fLongitude;
            }
            set {
                SetPropertyValue("Longitude", ref fLongitude, value);
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            this.Created = Constants.DateTimeTimeZone(this.Session);
        }

        protected override void OnSaving()
        {
            WorkFlowHelper.CreateWorkFlowProcess(this);

            base.OnSaving();
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<WorkFlowSignature>().Execute(this);

            base.OnSaved();
        }
    }
}
