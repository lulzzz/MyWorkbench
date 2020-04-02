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
    public class WorkFlowProcessTracking : BaseObject
    {
        public WorkFlowProcessTracking(Session session)
            : base(session)
        {
        }

        private WorkFlowProcess fWorkFlowProcess;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Association("WorkFlowProcess_WorkFlowProcessTracking")]
        public WorkFlowProcess WorkFlowProcess {
            get {
                return fWorkFlowProcess;
            }
            set {
                SetPropertyValue("WorkFlowProcess", ref fWorkFlowProcess, value);
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

        private string fDescription;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.DateReceived = Constants.DateTimeTimeZone(this.Session);
        }
    }
}
