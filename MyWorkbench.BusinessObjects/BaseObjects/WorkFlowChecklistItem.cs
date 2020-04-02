using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Common;

namespace MyWorkbench.BusinessObjects.BaseObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("Label")]
    [NavigationItem(false)]
    public class WorkFlowChecklistItem : BaseObject, IModal
    {
        public WorkFlowChecklistItem(Session session)
            : base(session) {
        }

        private WorkFlowChecklist fWorkFlowChecklist;
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("WorkFlowChecklist_WorkFlowChecklistItem")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WorkFlowChecklist WorkFlowChecklist {
            get {
                return fWorkFlowChecklist;
            }
            set {
                SetPropertyValue("WorkFlowChecklist", ref fWorkFlowChecklist, value);
            }
        }

        private int? fOrder;
        [RuleRequiredField(DefaultContexts.Save)]
        [ToolTip("The order you would like the fields to appear on the checklist")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public int? Order {
            get {
                return fOrder;
            }
            set {
                SetPropertyValue("Order", ref fOrder, value);
            }
        }

        private string fLabel;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ToolTip("The label of the checklist item")]
        [Size(SizeAttribute.Unlimited)]
        public string Label {
            get {
                return fLabel;
            }
            set {
                SetPropertyValue("Label", ref fLabel, value);
            }
        }

        private string fUnits;
        [ToolTip("If the item is a number then what units is it measured in. e.g. each, m, kpa")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string Units {
            get {
                return fUnits;
            }
            set {
                SetPropertyValue("Units", ref fUnits, value);
            }
        }

        private DataType fDataType;
        [ToolTip("What type of data will be entered into the field")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public DataType DataType {
            get {
                return fDataType;
            }
            set {
                SetPropertyValue("DataType", ref fDataType, value);
            }
        }

        private string fValue;
        [ToolTip("Value of the data")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string Value {
            get {
                return fValue;
            }
            set {
                SetPropertyValue("DataType", ref fValue, value);
            }
        }

        private CheckListGroup fCheckListGroup;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public CheckListGroup CheckListGroup {
            get {
                return fCheckListGroup;
            }
            set {
                SetPropertyValue("CheckListGroup", ref fCheckListGroup, value);
            }
        }
    }
}