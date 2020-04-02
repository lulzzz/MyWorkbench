using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.BaseObjects;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("Action_Debug_Stop")]
    public class FaultType : BaseObject {
        public FaultType(Session session)
            : base(session) {
        }

        #region Properties
        private string fDescription;
        [Indexed("GCRecord", Unique = true)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Fault", ref fDescription, value);
            }
        }

        [Association("FaultType_WorkFlowEquipment")]
        public XPCollection<WorkFlowEquipment> WorkFlowEquipments {
            get {
                return GetCollection<WorkFlowEquipment>("WorkFlowEquipments");
            }
        }

        #endregion
    }
}
