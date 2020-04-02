using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Reflection;
using DevExpress.ExpressApp.Web;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using System;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;

namespace MyWorkbench.Module.Web.Controllers {
    public class WorkflowDetailViewController : ObjectViewController {
        private System.ComponentModel.IContainer _components = null;
        private SingleChoiceAction _convert;

        private IEnumerable<TypeInfo> _types = null;
        public IEnumerable<TypeInfo> Types {
            get {
                if (this._types == null) {
                    var currentAssembly = System.Reflection.Assembly.Load("MyWorkbench.BusinessObjects");
                    this._types = currentAssembly.DefinedTypes.Where(type => type.IsSubclassOf(typeof(WorkflowBase))).OrderBy(g => g.Name).ToList();
                }

                return this._types;
            }
        }

        public WorkflowBase SourceObject { get; set; }
        public WorkflowBase TargetObject { get; set; }
        public bool TargetObjectSaved { get; set; }

        #region Constructor
        public WorkflowDetailViewController() {
            this.TargetObjectType = typeof(WorkflowBase);
            this.TypeOfView = typeof(DetailView);
            this.InitializeComponent();
            this.RegisterActions(this._components);
        }
        #endregion

        #region Methods
        private void InitializeComponent() {
            this._components = new System.ComponentModel.Container();

            this._convert = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this._components) {
                SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent,
                Id = "ConvertController",
                TargetObjectsCriteria = "Not IsNewObject(This)",
                ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation,
                ImageName = "BO_Folder",
                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image,
                Caption = "Convert your workflow to another workflow",
                Category = "Edit",
            };

            this._convert.Execute += _convert_Execute;

            Types.ForEach(AddChoiceActionItem);
        }

        private void _convert_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            Session session = ((XPObjectSpace)objectSpace).Session;
            this.SourceObject = session.FindObject(this.View.CurrentObject.GetType(), CriteriaOperator.Parse("Oid == ?", (this.View.CurrentObject as BaseObject).Oid)) as WorkflowBase;
            this.TargetObject = IWorkflowHelper.Convert(session, (this._convert.SelectedItem.Data as Type), this.SourceObject);

            if (this.TargetObject != null)
                this.RedirectToDetailView(objectSpace);
        }

        private void AddChoiceActionItem(TypeInfo TypeInfo) {
            ChoiceActionItem choiceActionItem = new ChoiceActionItem(TypeInfo.Name, TypeInfo);
            this._convert.Items.Add(choiceActionItem);
        }

        private void RedirectToDetailView(IObjectSpace ObjectSpace) {
            DetailView view = WebApplication.Instance.CreateDetailView(ObjectSpace, this.TargetObject, true);
            view.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            view.ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
            TargetObjectSaved = false;

            this.Frame.SetView(view, true, this.Frame, false);
        }

        private void ObjectSpace_ObjectSaved(object sender, ObjectManipulatingEventArgs e)
        {
            IWorkflowHelper.ConvertSaved(this.TargetObject, this.SourceObject);
            TargetObjectSaved = true;
        }

        private void SetActive() {
            foreach (ChoiceActionItem choiceActionItem in this._convert.Items) {
                PermissionHelper.UpdatePermissionVisibility(choiceActionItem, ObjectSpace);
            }
        }

        protected override void OnActivated() {
            base.OnActivated();

            this.SetActive();
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing) {
            if (disposing && (_components != null)) {
                this._components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
