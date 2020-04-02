using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace MyWorkbench.BusinessObjects.Common
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("GroupName")]
    [NavigationItem("Lookups")]
    public class CheckListGroup : BaseObject {
        public CheckListGroup(Session session)
            : base(session) {
        }

        private int fOrder;
        private string fGroupName;

        [RuleUniqueValue]
        [RuleRequiredField(DefaultContexts.Save)]
        public string GroupName {
            get {
                return fGroupName;
            }
            set {
                SetPropertyValue("GroupName", ref fGroupName, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ToolTip("The order you would like the groups to appear on the checklist")]
        public int Order {
            get {
                return fOrder;
            }
            set {
                SetPropertyValue("Order", ref fOrder, value);
            }
        }

        [Association("CheckListGroup-CheckListItems")]
        public XPCollection<ChecklistItem> CheckListItems {
            get {
                return GetCollection<ChecklistItem>("CheckListItems");
            }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 320, DetailViewImageEditorFixedHeight = 320)]
        public System.Drawing.Image Image { get; set; }

        public override void AfterConstruction() {
            base.AfterConstruction();
        }
    }
}