using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Ignyt.BusinessInterface;

namespace MyWorkbench.BusinessObjects.Common
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [NavigationItem(false)]
    public class ChecklistItem : BaseObject
    {
        public ChecklistItem(Session session)
            : base(session)
        {
        }

        private CheckListGroup fCheckListGroup;
        private Checklist fChecklist;
        private DataType fDataType;
        private string fUnits;
        private string fLabel;
        private int? fOrder;

        [RuleRequiredField(DefaultContexts.Save)]
        [Association("Checklist-ChecklistItems")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Checklist Checklist {
            get {
                return fChecklist;
            }
            set {
                SetPropertyValue("CheckList", ref fChecklist, value);
            }
        }


        [Association("CheckListGroup-CheckListItems")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CheckListGroup CheckListGroup {
            get {
                return fCheckListGroup;
            }
            set {
                SetPropertyValue("CheckListGroup", ref fCheckListGroup, value);
            }
        }

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
    }
}