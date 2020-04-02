using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using System;
using System.Collections;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using MyWorkbench.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using DevExpress.Persistent.BaseImpl;

namespace MyWorkbench.Module.Web.Controllers {
    public class CustomStatusPriorityController : ObjectViewController {
        private System.ComponentModel.IContainer _components = null;
        private SingleChoiceAction _setStatusPriority;
        private ChoiceActionItem _setPriorityItem;
        private ChoiceActionItem _setStatusItem;

        #region Constructor
        public CustomStatusPriorityController() {
            this.TargetObjectType = typeof(IStatusPriority<Status,Priority>);
            this.TypeOfView = typeof(ObjectView);
            this.InitializeComponent();
            this.RegisterActions(this._components);
        }
        #endregion

        #region Methods
        private void InitializeComponent() {
            this._components = new System.ComponentModel.Container();

            this._setStatusPriority = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this._components) {
                Caption = "Status",
                Category = DevExpress.Persistent.Base.PredefinedCategory.Edit.ToString(),
                SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent,
                Id = "StatusPriorityController",
                ImageName = "BO_Report",
                TargetObjectsCriteria = "[Oid] Is Not Null",
                ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation
            };

            this._setStatusPriority.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.SetReportAction_Execute);
            this.Activated += new System.EventHandler(this.StatusPriorityActionsController_Activated);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);

            this._setPriorityItem = new ChoiceActionItem(CaptionHelper.GetMemberCaption(this.TargetObjectType, "Priority"), null);
            this._setStatusPriority.Items.Add(this._setPriorityItem);
            FillItemWithEnumValues(this._setPriorityItem, typeof(Priority));

            this._setStatusItem = new ChoiceActionItem(CaptionHelper.GetMemberCaption(this.TargetObjectType, "Status"), null);
            this._setStatusPriority.Items.Add(this._setStatusItem);
        }

        protected override void OnActivated() {
            FillItemWithValues(this._setStatusItem);
        }

        private void UpdateStatusPriorityActionState() {
            var isGranted = true;

            foreach (object selectedObject in View.SelectedObjects) {
                var objectHandle = ObjectSpace.GetObjectHandle(selectedObject);

                var isPriorityGranted = SecuritySystem.IsGranted(new PermissionRequest(this.View.ObjectSpace, this.TargetObjectType, SecurityOperations.Write, "Priority"));
                var isStatusGranted = SecuritySystem.IsGranted(new PermissionRequest(this.View.ObjectSpace, this.TargetObjectType, SecurityOperations.Write, "Status"));

                if (!isPriorityGranted || !isStatusGranted) {
                    isGranted = false;
                }
            }
            this._setStatusPriority.Enabled.SetItemValue("SecurityAllowance", isGranted);
        }

        private void FillItemWithValues(ChoiceActionItem parentItem) {
            var objectSpace = Application.CreateObjectSpace();
            parentItem.Items.Clear();
            Session currentSession = ((XPObjectSpace)objectSpace).Session;

            DevExpress.Xpo.Metadata.XPClassInfo statusClass = currentSession.GetClassInfo(typeof(Status));
            SortingCollection sortProps = new SortingCollection(null) {
                new SortProperty("Description", DevExpress.Xpo.DB.SortingDirection.Ascending)
            };

            foreach (Status status in currentSession.GetObjects(statusClass, null, sortProps, 0, false, true)) {
                var item = new ChoiceActionItem(status.Description, status);
                parentItem.Items.Add(item);
            }
        }

        private static void FillItemWithEnumValues(ChoiceActionItem parentItem, Type enumType) {
            foreach (object current in Enum.GetValues(enumType)) {
                var ed = new EnumDescriptor(enumType);
                var item = new ChoiceActionItem(ed.GetCaption(current), current) { ImageName = ImageLoader.Instance.GetEnumValueImageName(current) };
                parentItem.Items.Add(item);
            }
        }
        #endregion

        #region Events
        private void View_SelectionChanged(object sender, EventArgs e) {
            UpdateStatusPriorityActionState();
        }

        private void StatusPriorityActionsController_Activated(object sender, EventArgs e) {
            View.SelectionChanged += View_SelectionChanged;
            UpdateStatusPriorityActionState();
        }

        private void SetReportAction_Execute(object sender, SingleChoiceActionExecuteEventArgs args) {
            var objectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            var objectsToProcess = new ArrayList(args.SelectedObjects);

            if (args.SelectedChoiceActionItem.ParentItem == this._setPriorityItem) {
                foreach (Object obj in objectsToProcess) {
                    var objInNewObjectSpace = (IStatusPriority<Status,Priority>)objectSpace.GetObject(obj);
                    objInNewObjectSpace.Priority = (Priority)args.SelectedChoiceActionItem.Data;
                    (objInNewObjectSpace as BaseObject).Save();
                }
            } else {
                if (args.SelectedChoiceActionItem.ParentItem == this._setStatusItem) {
                    foreach (Object obj in objectsToProcess) {
                        var objInNewObjectSpace = (IStatusPriority<Status, Priority>)objectSpace.GetObject(obj);
                        objInNewObjectSpace.Status = (Status)objectSpace.GetObject(args.SelectedChoiceActionItem.Data);
                        (objInNewObjectSpace as BaseObject).Save();
                    }
                }
            }

            objectSpace.CommitChanges();

            View.ObjectSpace.Refresh();
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
