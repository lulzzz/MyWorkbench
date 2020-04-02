using DevExpress.Utils;
using System;

namespace Ignyt.BusinessInterface {
    [AttributeUsage(AttributeTargets.Property)]
    public class EnableInlineEdit : Attribute {
        public DefaultBoolean visible;

        public EnableInlineEdit(DefaultBoolean Visible) {
            this.visible = Visible;
        }
    }
}
