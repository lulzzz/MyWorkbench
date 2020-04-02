using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Utils;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [ImageName("BO_Resume")]
    [NavigationItem("Settings")]
    public class CustomField : BaseObject {
        public CustomField(Session session)
            : base(session) {
        }

        private Type fCustomType;
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter<ICustomizable>))]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Type")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Type CustomType {
            get {
                return fCustomType;
            }
            set {
                SetPropertyValue("CustomType", ref fCustomType, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        public string Type {
            get {
                return this.CustomType.Name;
            }
        }

        private DataType fDataType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Data Type")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DataType DataType {
            get {
                return fDataType;
            }
            set {
                SetPropertyValue("DataType", ref fDataType, value);
            }
        }

        private string fName;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Name {
            get => fName;
            set => SetPropertyValue(nameof(Name), ref fName, value);
        }

        private bool fIsRequired;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public bool IsRequired {
            get => fIsRequired;
            set => SetPropertyValue(nameof(IsRequired), ref fIsRequired, value);
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Description {
            get {
                return null;
            }
        }
    }
}
