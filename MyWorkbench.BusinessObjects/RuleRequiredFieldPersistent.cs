using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Reflection;
using DevExpress.Xpo.Metadata;

namespace MyWorkbench.BusinessObjects {
    [DefaultProperty("Value")]
    [NonPersistent]
    public class ObjectProperty : BaseObject {
        public ObjectProperty(Session session)
            : base(session) {
        }

        private string fValue;
        private string fDisplayName;

        public string DisplayName {
            get {
                return fDisplayName;
            }
            set {
                SetPropertyValue("DisplayName", ref fDisplayName, value);
            }
        }

        public string Value {
            get {
                return fValue;
            }
            set {
                SetPropertyValue("Value", ref fValue, value);
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
        }
    }

    [DefaultClassOptions]
    [NavigationItem("Settings")]
    public class RuleRequiredFieldPersistent : BaseObject,
    DevExpress.Persistent.Validation.IRuleSource {
        public RuleRequiredFieldPersistent(Session session) : base(session) { }

        [RuleUniqueValue]
        [ToolTip("The unique name for the rule")]
        [RuleRequiredField]
        [Browsable(false)]
        public string Id {
            get { return GetPropertyValue<string>("Id"); }
            set { SetPropertyValue("Id", value); }
        }

        [ToolTip("Enter a description for the rule")]
        [RuleRequiredField]
        public string RuleName {
            get { return GetPropertyValue<string>("RuleName"); }
            set {
                SetPropertyValue("RuleName", value);
                SetPropertyValue("Id", value);
            }
        }

        [ToolTip("Enter the message you would like to see when the field is blank")]
        [RuleRequiredField]
        [DevExpress.Xpo.DisplayName("Message")]
        public string CustomMessageTemplate {
            get { return GetPropertyValue<string>("CustomMessageTemplate"); }
            set { SetPropertyValue("CustomMessageTemplate", value); }
        }

        [ToolTip("Skip validation when value is blank")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool SkipNullOrEmptyValues {
            get { return GetPropertyValue<bool>("SkipNullOrEmptyValues"); }
            set { SetPropertyValue("SkipNullOrEmptyValues", value); }
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool InvertResult {
            get { return GetPropertyValue<bool>("InvertResult"); }
            set { SetPropertyValue("InvertResult", value); }
        }

        [RuleRequiredField]
        [Browsable(false)]
        public string ContextIDs {
            get { return GetPropertyValue<string>("ContextIDs"); }
            set { SetPropertyValue("ContextIDs", value); }
        }

        [RuleRequiredField]
        [ToolTip("The name of the object to apply the rule to")]
        [Persistent("ObjectType")]
        protected string ObjectType {
            get {
                if (ObjectTypeCore != null) {
                    return ObjectTypeCore.FullName;
                }
                return "";
            }
            set { ObjectTypeCore = ReflectionHelper.FindType(value); }
        }

        [NonPersistent]
        [TypeConverter(typeof(DevExpress.Persistent.Base.LocalizedClassInfoTypeConverter))]
        [ImmediatePostData]
        [DevExpress.Xpo.DisplayName("Object")]
        public Type ObjectTypeCore {
            get { return GetPropertyValue<Type>("ObjectTypeCore"); }
            set { SetPropertyValue("ObjectTypeCore", value); }
        }

        [RuleRequiredField]
        [Browsable(false)]
        public string Property {
            get {
                return fProperty;
            }
            set {
                SetPropertyValue("Property", ref fProperty, value);
            }
        }

        [DataSourceProperty("ObjectProperties")]
        [NonPersistent]
        [DevExpress.Xpo.DisplayName("Property")]
        [ToolTip("The name of the field to apply the rule to")]
        public ObjectProperty PropertyDisplay {

            get {
                if (Property != null) {
                    //this finds the object in the list based on the persistent string value of Property
                    ObjectProperty objectProperty = ObjectProperties.Find(x => x.Value.Contains(Property));
                    if (objectProperty != null) return objectProperty;
                    else return null;
                } else return null;
            }
            set {
                SetPropertyValue<string>("Property", ref fProperty, value.ToString());
                if (!IsLoading && !IsSaving) {

                    OnChanged("ObjectTypeCore");
                }
            }
        }

        private string fProperty;
        private List<ObjectProperty> _ObjectProperty = new List<ObjectProperty>();
        //[Browsable(false)]
        public List<ObjectProperty> ObjectProperties {
            get { return _ObjectProperty; }
        }

        #region IRuleSource Members
        public System.Collections.Generic.ICollection<IRule> CreateRules() {
            System.Collections.Generic.List<IRule> list = new System.Collections.Generic.List<IRule>();
            RuleRequiredField rule = new RuleRequiredField();
            rule.Properties.SkipNullOrEmptyValues = this.SkipNullOrEmptyValues;
            rule.Properties.Id = this.Id;
            rule.Properties.InvertResult = this.InvertResult;
            rule.Properties.CustomMessageTemplate = this.CustomMessageTemplate;
            rule.Properties.TargetContextIDs = new ContextIdentifiers(this.ContextIDs).ToString();
            rule.Properties.TargetType = this.ObjectTypeCore;
            if (rule.Properties.TargetType != null) {
                foreach (PropertyInfo pi in rule.Properties.TargetType.GetProperties()) {
                    if (pi.Name == this.Property) {
                        rule.Properties.TargetPropertyName = pi.Name;
                    }
                }
            }
            for (int i = Validator.RuleSet.RegisteredRules.Count - 1; i >= 0; i--) {
                if (Validator.RuleSet.RegisteredRules[i].Id == this.Id) {
                    Validator.RuleSet.RegisteredRules.RemoveAt(i);
                }
            }
            list.Add(rule);
            return list;
        }
        [Browsable(false)]
        public string Name {
            get { return this.RuleName; }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
            InvertResult = false;
            SkipNullOrEmptyValues = false;
            ContextIDs = "Save";
        }

        #endregion

        #region Methods

        public void Getproperties(Type type) {
            Session session = this.Session;
            //Type type = fObject.GetType().;

            XPClassInfo cinfo = session.GetClassInfo(type);
            ObjectProperties.Clear();
            foreach (XPMemberInfo mi in cinfo.PersistentProperties) {
                ObjectProperties.Add(new ObjectProperty(this.Session) {
                    Value = mi.Name,
                    DisplayName = mi.DisplayName
                });
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "ObjectTypeCore")
                if (oldValue != newValue & newValue != null) {
                    Getproperties(ObjectTypeCore);
                }
        }
        #endregion
    }
}
