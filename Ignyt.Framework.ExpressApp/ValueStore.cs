using DevExpress.Persistent.Base;
using System.Collections.Generic;

namespace Ignyt.Framework.ExpressApp {
    public class ValueStore {
        private static IValueManager<Dictionary<string, object>> _store;

        public static void Store(string Key) {
            _store = ValueManager.GetValueManager<Dictionary<string, object>>(Key);
        }

        public static void Add(string Key, object Value) {
            Store(Key);

            _store.Value = new Dictionary<string, object> { { Key, Value } };
        }

        public static object Get(string Key) {
            Store(Key);

            if (_store.Value == null)
                _store.Value = new Dictionary<string, object>();

            _store.Value.TryGetValue(Key, out object value);

            return value;
        }
    }
}
