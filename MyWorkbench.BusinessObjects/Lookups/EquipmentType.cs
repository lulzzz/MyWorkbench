using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Inventory;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("Action_Debug_Stop")]
    public class EquipmentType : BaseObject {
        public EquipmentType(Session session)
            : base(session) {
        }

        #region Properties
        private string fDescription;
        [Indexed("GCRecord", Unique = true)]
        [RuleUniqueValue]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        [Association("EquipmentType_Equipment")]
        public XPCollection<Equipment> Equipments {
            get {
                return GetCollection<Equipment>("Equipments");
            }
        }
        #endregion
    }
}
