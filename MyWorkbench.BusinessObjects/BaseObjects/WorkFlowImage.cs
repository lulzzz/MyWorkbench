using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using System;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowImage : BaseObject, IWorkflowImage<WorkflowBase>, IModal
    {
        public WorkFlowImage(Session session)
            : base(session) {
        }

        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowImage")]
        [VisibleInListView(false),VisibleInDetailView(false)]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private WorkFlowTask fWorkFlowTask;
        [Association("WorkFlowTask_WorkFlowImage")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WorkFlowTask WorkFlowTask {
            get { return fWorkFlowTask; }
            set {
                SetPropertyValue("WorkFlowTask", ref fWorkFlowTask, value);
            }
        }

        private System.Drawing.Image fImage;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 640, DetailViewImageEditorFixedHeight = 480)]
        public System.Drawing.Image Image {
            get { return fImage; }
            set {
                SetPropertyValue("Image", ref fImage, value);
            }
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Description {
            get { return fDescription; }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private DateTime fCreated;
        [VisibleInDetailView(false),VisibleInListView(true)]
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
    }
}
