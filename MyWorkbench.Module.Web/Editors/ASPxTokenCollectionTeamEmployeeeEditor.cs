using System;
using System.Collections;
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
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using MyWorkbench.BusinessObjects.DomainComponent;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.Module.Web.Editors {
    [PropertyEditor(typeof(XPBaseCollection),"TeamEmployeeTokenCollectionEditor", false)]
    public class ASPxTokenCollectionTeamEmployeeeEditor : ASPxPropertyEditor, IComplexViewItem {
        public ASPxTokenCollectionTeamEmployeeeEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        private ASPxTokenBox _Control;
        private XPObjectSpace _ObjectSpace;
        private XafApplication _Application;
        private XPBaseCollection _tokenItems;

        BindingList<TeamEmployeeDisplay> teamEmployeeDisplayList = new BindingList<TeamEmployeeDisplay>();

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


            _Control.TokensChanged += control_TokensChanged;

            return _Control;
        }

        protected override WebControl CreateViewModeControlCore() {
            _Control = new ASPxTokenBox {
                ClientEnabled = false,
                Width = new Unit("100%")
            };
            return _Control;
        }

        protected override void ReadValueCore() {
            base.ReadValueCore();

            _Control = (ASPxTokenBox)(ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit ? Editor : InplaceViewModeEditor);

            if (_Control != null) {
                _Control.TokensChanged -= new EventHandler(control_TokensChanged);

                teamEmployeeDisplayList.Clear();

                DevExpress.Xpo.SortingCollection sortProps;

                sortProps = new SortingCollection(null) {
                    new SortProperty("Description", DevExpress.Xpo.DB.SortingDirection.Ascending)
                };


                DevExpress.Xpo.Metadata.XPClassInfo teamClass;
                teamClass = _ObjectSpace.Session.GetClassInfo(typeof(Team));
                ICollection teamList = _ObjectSpace.Session.GetObjects(teamClass, null, sortProps, 0, false, true);

                foreach (Team team in teamList) {
                    teamEmployeeDisplayList.Add(new TeamEmployeeDisplay() { ID = team.Oid, DisplayName = team.Description, Team = team });
                }

                DevExpress.Xpo.Metadata.XPClassInfo employeeClass;
                sortProps.Clear();
                sortProps.Add(new SortProperty("LastName", DevExpress.Xpo.DB.SortingDirection.Ascending));
                employeeClass = _ObjectSpace.Session.GetClassInfo(typeof(Employee));
                ICollection employeeList = _ObjectSpace.Session.GetObjects(employeeClass, null, sortProps, 0, false, true);

                foreach (Employee employee in employeeList) {
                    teamEmployeeDisplayList.Add(new TeamEmployeeDisplay() { ID = employee.Oid, DisplayName = employee.FullName, Employee = employee });
                }

                //tokenItems = teamEmployeeDisplayList;            
                BindingList<TeamEmployeeDisplay> dataSource = teamEmployeeDisplayList;
                _Control.DataSource = dataSource;
                _Control.TextField = "DisplayName";
                _Control.ValueField = "ID";
                _Control.ItemValueType = typeof(Guid);
                _Control.DataBind();

                //read the objects in the collection to display them
                _tokenItems = (XPBaseCollection)PropertyValue;

                if(_tokenItems != null)
                    foreach (object obj in _tokenItems) {
                        _Control.Tokens.Add(((XPBaseObject)obj).GetMemberValue("Description").ToString());
                    }

                _Control.TokensChanged += new EventHandler(control_TokensChanged);
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

        private object GetObject(TeamEmployeeDisplay Item, object CurrentObjectValue) {
            IList objects = CurrentObject.GetPropertyValue("TeamEmployees") as IList;
            //IList objects = _ObjectSpace.GetObjects(MemberInfo.ListElementTypeInfo.Type, new BinaryOperator(CurrentObject.GetType().Name, CurrentObjectValue), true);            

            foreach (object obj in objects) {
                if (Item.Employee != null) {
                    if (obj.GetPropertyValue("Employee") != null && (obj.GetPropertyValue("Employee") as Employee).Oid == Item.ID)
                        return obj;
                } else if (Item.Team != null) {
                    if (obj.GetPropertyValue("Team") != null && (obj.GetPropertyValue("Team") as Team).Oid == Item.ID)
                        return obj;
                }
            }

            return null;
        }

        private object CreateObject(TeamEmployeeDisplay Item, object CurrentObjectValue) {
            object obj = null;

            obj = _ObjectSpace.CreateObject(MemberInfo.ListElementTypeInfo.Type);

            if (Item.Employee != null)
                obj.SetPropertyValue("Employee", _ObjectSpace.FindObject<Employee>(new BinaryOperator("Oid", Item.ID)));
            else
                obj.SetPropertyValue("Team", _ObjectSpace.FindObject<Team>(new BinaryOperator("Oid", Item.ID)));

            obj.SetPropertyValue(CurrentObject.GetType().BaseType.Name, _ObjectSpace.FindObject(CurrentObject.GetType(), new BinaryOperator("Oid", ((BaseObject)CurrentObject).Oid)));

            return obj;
        }

        private void control_TokensChanged(object sender, EventArgs e) {
            ASPxTokenBox control = (ASPxTokenBox)sender;

            foreach (ListEditItem item in control.Items) {
                object obj = GetObject(teamEmployeeDisplayList.Where(g => g.ID == Guid.Parse(item.Value.ToString())).FirstOrDefault(), ((BaseObject)CurrentObject).Oid);

                if (obj == null && ItemIsTagged((Guid)item.Value))
                    this._tokenItems.BaseAdd(CreateObject(teamEmployeeDisplayList.Where(g => g.ID == Guid.Parse(item.Value.ToString())).FirstOrDefault(), ((BaseObject)CurrentObject).Oid));
                else if (obj != null && !ItemIsTagged((Guid)item.Value))
                    this._tokenItems.BaseRemove(obj);
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