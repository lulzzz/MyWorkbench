using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class Label : BaseObject {
        public Label(Session session)
            : base(session) {
        }

        private string fDescription;
        [Size(200)]
        [RuleUniqueValue]
        [Indexed(Unique = true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int UniqueID {
            get {
                return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
            }
        }

        [Association("Label_WorkflowBase")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }
    }
}
