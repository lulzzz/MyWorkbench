using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkbench.BusinessObjects.Inventory {
    [DefaultProperty("Description")]
    [ImageName("Action_Debug_Stop")]
    public class ItemCategory : BaseObject {
        public ItemCategory(Session session)
            : base(session) {
        }

        private string fDescription;
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        [Association("ItemCategory_Item")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public XPCollection<Item> Items {
            get {
                return GetCollection<Item>("Items");
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
        }
    }
}
