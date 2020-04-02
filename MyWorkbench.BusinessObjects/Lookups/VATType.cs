using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem("Settings")]
    public class VATType : BaseObject {
        public VATType(Session session)
            : base(session)
        {
        }
        private string fName;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Name {
            get => fName;
            set => SetPropertyValue(nameof(Name), ref fName, value);
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private double fValue;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public double Value {
            get => fValue;
            set => SetPropertyValue(nameof(Value), ref fValue, value);
        }
    }
}
