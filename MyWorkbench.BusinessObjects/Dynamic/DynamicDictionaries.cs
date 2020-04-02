using DevExpress.Xpo;
using Ignyt.Framework.ExpressApp;
using Ignyt.BusinessInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Dynamic {
    public static class DynamicDictionaries {
        private static Dictionary<ulong, string> _recipientRoleTypes = new Dictionary<ulong, string>() { { 0, "Client" },
        { 1, "Employee" }, { 2, "Team" }, { 3, "Client Contact" }, { 4, "Creating User" }, { 5, "Supplier" }, { 6, "Supplier Contact" }};

        public static Dictionary<ulong, string> RecipientRoleTypes(Session Session) {
            foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                 .Where(mytype => mytype.GetInterfaces().Contains(typeof(IRecipientType)))) {
                if (new XpoHelper(Session).GetObjects(mytype, null) is ICollection<IRecipientType> collection)
                    InitializeRoleTypes(collection);
            }
            
            return _recipientRoleTypes;
        }

        private static void InitializeRoleTypes(IEnumerable<IRecipientType> Args) {
            foreach (IRecipientType recipientType in Args) {
                var max = from x in _recipientRoleTypes where x.Value == _recipientRoleTypes.Max(v => v.Value) select x.Key;

                KeyValuePair<ulong, string> value = new KeyValuePair<ulong, string>(max.FirstOrDefault(), recipientType.Description);
                _recipientRoleTypes.AddUnique(value);
            }
        }
    }
}
