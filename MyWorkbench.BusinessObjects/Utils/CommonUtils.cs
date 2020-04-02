using System;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Utils {
    public class TypeToStringConverter : ValueConverter {
        public override object ConvertFromStorageType(object stringObjectType) {
            return ReflectionHelper.FindType((string)stringObjectType);
        }
        public override object ConvertToStorageType(object objectType) {
            if (objectType == null) {
                return null;
            }
            return ((Type)objectType).FullName;
        }
        public override Type StorageType {
            get { return typeof(string); }
        }
    }

    public class LocalizedClassInfoTypeConverter<T> : LocalizedClassInfoTypeConverter {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
            List<Type> list = new List<Type>();
            foreach (Type t in base.GetStandardValues(context)) {
                if (typeof(T).IsAssignableFrom(t)) list.Add(t);
            }
            return new StandardValuesCollection(list);
        }
    }

    public class LocalizedClassInfoTypeConverter<T, U> : LocalizedClassInfoTypeConverter {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
            List<Type> list = new List<Type>();
            foreach (Type t in base.GetStandardValues(context)) {
                if (typeof(T).IsAssignableFrom(t)) list.Add(t);
                else if (typeof(U).IsAssignableFrom(t)) list.Add(t);
            }
            return new StandardValuesCollection(list);
        }
    }
}
