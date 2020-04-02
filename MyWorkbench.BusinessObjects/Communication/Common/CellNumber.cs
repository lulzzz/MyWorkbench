using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Communication.Common
{
    [DefaultProperty("DisplayValue")]
    [NavigationItem(false)]
    public class CellNumber : BaseObject, ICellNumber {
        public CellNumber(Session session)
            : base(session) {
        }

        public RecipientRoles RecipientRole { get; set; }

        public string FullName { get; set; }

        public string CellNo { get; set; }

        public string DisplayValue {
            get {
                return string.Concat(FullName, "<", CellNo, ">");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public KeyValuePair<string, string> KeyValuePair {
            get {
                return new KeyValuePair<string, string>(this.CellNo, this.FullName);
            }
        }
    }
}
