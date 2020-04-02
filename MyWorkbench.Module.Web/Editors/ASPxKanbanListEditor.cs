using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using System.Collections.Generic;
using DevExpress.ExpressApp.Utils;
using System.Collections;
using Ignyt.BusinessInterface.Kanban;
using DevExpress.ExpressApp.Xpo;
using MyWorkbench.BusinessObjects.Lookups;
using System.Linq;

namespace MyWorkbench.Module.Web.Editors
{
    [ListEditor(typeof(IKanban), false)]
    public class ASPxKanbanListEditor : ListEditor, IComplexListEditor
    {
        public ASPxKanbanListEditor(IModelListView info) : base(info) { }

        private Syncfusion.JavaScript.Web.Kanban _control;
        private IObjectSpace _objectSpace;
        private XafApplication _application;
        DevExpress.ExpressApp.CollectionSourceBase _collectionSourceBase;
        public IEnumerable<Status> _statuses;

        protected override object CreateControlsCore()
        {
            _control = new Syncfusion.JavaScript.Web.Kanban
            {
                ID = "KanbanListEditor_control",
            };

            return _control;
        }

        protected override void AssignDataSourceToControl(Object dataSource)
        {
            if (_control != null)
            {
                _control.DataSource = ListHelper.GetList(dataSource);
                _control.Fields.Content = "IDescription";
                _control.Fields.ImageUrl = "ImageUrl";
                _control.Fields.PrimaryKey = "Oid";
                _control.KeyField = "IStatus";

                foreach (Status status in this._statuses)
                {
                    _control.Columns.Add(new Syncfusion.JavaScript.Models.KanbanColumn() { HeaderText = status.Description, Key = new List<string>() { status.Description } });
                }

                _control.DataBind();
            }
        }

        public override SelectionType SelectionType {
            get { return SelectionType.TemporarySelection; }
        }

        public override IList GetSelectedObjects()
        {
            List<object> selectedObjects = new List<object>();
            if (FocusedObject != null)
            {
                selectedObjects.Add(FocusedObject);
            }
            return selectedObjects;
        }

        public override DevExpress.ExpressApp.Templates.IContextMenuTemplate ContextMenuTemplate {
            get { return null; }
        }

        public override void Refresh()
        {
            if (_control != null)
            {
                _control.DataBind();
            }
        }

        private object focusedObject;
        public override object FocusedObject {
            get {
                return focusedObject;
            }
            set {
                focusedObject = value;
            }
        }

        public override void BreakLinksToControls()
        {
            _control = null;
            base.BreakLinksToControls();
        }

        public void Setup(DevExpress.ExpressApp.CollectionSourceBase CollectionSource, DevExpress.ExpressApp.XafApplication Application)
        {
            _collectionSourceBase = CollectionSource;
            _objectSpace = Application.CreateObjectSpace();
            _application = Application;
            this._statuses = this._objectSpace.GetObjects<Status>(null);
        }
    }
}