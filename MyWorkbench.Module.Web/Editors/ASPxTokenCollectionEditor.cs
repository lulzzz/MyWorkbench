using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.Web;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Linq;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Xpo;

namespace MyWorkbench.Module.Web.Editors {
    [PropertyEditor(typeof(XPBaseCollection), "TokenCollectionEditor", false)]
    public class ASPxTokenCollectionEditor : ASPxPropertyEditor, IComplexViewItem {
        public ASPxTokenCollectionEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        private ASPxTokenBox _Control;
        private XPObjectSpace _ObjectSpace;
        private XafApplication _Application;

        protected override WebControl CreateEditModeControlCore() {
            _Control = new ASPxTokenBox {
                ID = "ASPxTokenBox_control",
                TextField = this.ObjectTypeInfo.DefaultMember.Name,
                ValueField = this.ObjectTypeInfo.KeyMember.Name,
                IncrementalFilteringMode = IncrementalFilteringMode.Contains,
                ShowDropDownOnFocus = ShowDropDownOnFocusMode.Always,
                AllowCustomTokens = false,
                Width = new Unit("100%"),
                AnimationType = AnimationType.Fade,
                EnableCallbackMode = true,
                CallbackPageSize = 50,
                ShowShadow = true
            };

            _Control.ClientSideEvents.TokensChanged = @"function (s, e) { setTimeout(function() { s.GetInputElement().blur(); }, 200);  }";

            _Control.TokensChanged += Control_TokensChanged;

            return _Control;
        }

        protected override WebControl CreateViewModeControlCore() {
            _Control = new ASPxTokenBox {
                ClientEnabled = false,
                Width = new Unit("100%")
            };
            return _Control;
        }

        XPBaseCollection tokenItems;

        protected override void ReadValueCore() {
            base.ReadValueCore();

            var collectionType = this.MemberInfo.MemberType.GenericTypeArguments[0];
            var collectionTypeInfo = (DevExpress.ExpressApp.DC.TypeInfo)XafTypesInfo.Instance.FindTypeInfo(collectionType.FullName);

            if (PropertyValue is XPBaseCollection) {
                _Control = (ASPxTokenBox)(ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit ? Editor : InplaceViewModeEditor);

                if (_Control != null) {
                    _Control.TokensChanged -= new EventHandler(Control_TokensChanged);
                    tokenItems = (XPBaseCollection)PropertyValue;
                    XPCollection dataSource = new XPCollection(tokenItems.Session, MemberInfo.ListElementType);
                    IModelClass classInfo = _Application.Model.BOModel.GetClass(MemberInfo.ListElementTypeInfo.Type);
                    if (tokenItems.Sorting.Count > 0) {
                        dataSource.Sorting = tokenItems.Sorting;
                    } else if (!String.IsNullOrEmpty(classInfo.DefaultProperty)) {
                        dataSource.Sorting.Add(new SortProperty(classInfo.DefaultProperty, DevExpress.Xpo.DB.SortingDirection.Ascending));
                    }
                    _Control.DataSource = dataSource;
                    _Control.TextField = classInfo.DefaultProperty;
                    _Control.ValueField = classInfo.KeyProperty;
                    _Control.ItemValueType = classInfo.TypeInfo.KeyMember.MemberType;
                    _Control.DataBind();

                    foreach (object obj in tokenItems) {
                        _Control.Tokens.Add(((XPBaseObject)obj).GetMemberValue(MemberInfo.ListElementTypeInfo.DefaultMember.Name).ToString());
                    }
                    _Control.TokensChanged += new EventHandler(Control_TokensChanged);
                }
            }
        }

        private bool ItemIsTagged(Guid value) {
            bool tagged = false;
            if (_Control.Value != null && !string.IsNullOrEmpty(_Control.Value.ToString())) {
                List<Guid> values = _Control.Value.ToString().Split(',').Select(Guid.Parse).ToList<Guid>();

                if (values.Contains(value)) {
                    tagged = true;
                }
            }

            return tagged;
        }

        private void Control_TokensChanged(object sender, EventArgs e) {
            ASPxTokenBox control = (ASPxTokenBox)sender;

            foreach (ListEditItem item in control.Items) {
                object obj = _ObjectSpace.GetObjectByKey(MemberInfo.ListElementTypeInfo.Type, item.Value);
                if (ItemIsTagged((Guid)item.Value)) {
                    tokenItems.BaseAdd(obj);
                } else {
                    tokenItems.BaseRemove(obj);
                }
            }
            OnControlValueChanged();
            _ObjectSpace.SetModified(CurrentObject);
        }

        public new ASPxTokenBox Editor {
            get {
                return (ASPxTokenBox)base.Editor;
            }
        }

        public new ASPxTokenBox InplaceViewModeEditor {
            get {
                return (ASPxTokenBox)base.InplaceViewModeEditor;
            }
        }

        protected override void SetImmediatePostDataScript(string script) {
            Editor.ClientSideEvents.TokensChanged = script;
        }

        protected override bool IsMemberSetterRequired() {
            return false;
        }

        public void Setup(IObjectSpace objectSpace, XafApplication application) {
            _ObjectSpace = objectSpace as XPObjectSpace;
            _Application = application;
        }
    }
}