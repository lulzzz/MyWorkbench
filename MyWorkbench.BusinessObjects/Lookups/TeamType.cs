using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Employee")]
    public class TeamType : BaseObject {
        public TeamType(Session session)
            : base(session) {
        }

        private string fDescription;
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }
    }
}
