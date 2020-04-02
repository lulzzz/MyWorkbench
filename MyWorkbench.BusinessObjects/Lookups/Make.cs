using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Inventory;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem("Lookups")]
    [ImageName("Action_Debug_Stop")]
    public class Model : BaseObject {
        public Model(Session session)
            : base(session) {
        }

        #region Properties
        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
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

        private Make fMake;
        [Association("Make_Model")]
        [VisibleInListView(true),VisibleInDetailView(true)]
        public Make Make {
            get {
                return fMake;
            }
            set {
                SetPropertyValue("Make", ref fMake, value);
            }
        }

        [Association("Model_Equipment")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public XPCollection<Equipment> Equipments {
            get {
                return GetCollection<Equipment>("Equipments");
            }
        }
        #endregion
    }

    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem("Lookups")]
    public class Make : BaseObject {
        public Make(Session session)
            : base(session) {
        }

        #region Properties
        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
        [Indexed("GCRecord", Unique = true)]
        [RuleUniqueValue]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Name", ref fDescription, value);
            }
        }

        [Association("Make_Equipment")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public XPCollection<Equipment> Equipments {
            get {
                return GetCollection<Equipment>("Equipments");
            }
        }

        [Association("Make_Model")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public XPCollection<Model> Models {
            get {
                return GetCollection<Model>("Models");
            }
        }
        #endregion
    }
}
