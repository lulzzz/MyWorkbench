using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("Action_Debug_Stop")]
    public class Source : BaseObject {
        public Source(Session session)
            : base(session) {
        }

        private string fSourceName;
        [XafDisplayName("Source")]
        public string SourceName {
            get => fSourceName;
            set => SetPropertyValue(nameof(SourceName), ref fSourceName, value);
        }

        [Association("Source_Vendor")]
        public XPCollection<Vendor> Vendors {
            get {
                return GetCollection<Vendor>("Vendors");
            }
        }
    }
}
