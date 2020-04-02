using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultProperty("Description")]
    [NavigationItem("Lookups")]
    [ImageName("Action_Debug_Stop")]
    public class Status : BaseObject, IStatus {
        public Status(Session session)
            : base(session) {
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        [Persistent("Color")]
        private Int32 color;
        [NonPersistent]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public System.Drawing.Color Color {
            get { return System.Drawing.Color.FromArgb(color); }
            set {
                color = value.ToArgb();
                OnChanged("Color");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int UniqueID {
            get {
                return BitConverter.ToInt32(this.Oid.ToByteArray(), 0);
            }
        }

        private bool fIsCompleted;
        [VisibleInDetailView(true),VisibleInListView(true)]
        public bool IsCompleted {
            get => fIsCompleted;
            set => SetPropertyValue(nameof(IsCompleted), ref fIsCompleted, value);
        }

        private bool fIsDefault;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public bool IsDefault {
            get => fIsDefault;
            set => SetPropertyValue(nameof(IsDefault), ref fIsDefault, value);
        }

        private WorkFlowType fWorkFlowType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public WorkFlowType WorkFlowType {
            get => fWorkFlowType;
            set => SetPropertyValue(nameof(WorkFlowType), ref fWorkFlowType, value);
        }


        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("Status_WorkflowBase")]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }
    }
}
