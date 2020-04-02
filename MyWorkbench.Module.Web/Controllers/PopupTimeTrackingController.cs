using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.Module.Web.Helpers;

namespace Diggit.Framework.Controllers {
    public class PopupTimeTrackingController : ObjectViewController<DetailView, ITimeTracking>
    {
        private System.ComponentModel.IContainer _components = null;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction _showTimeTracking;

        #region Properties
        private WorkFlowTimeTrackingMultiple _timeTrackingMultiple;
        private WorkFlowTimeTrackingMultiple TimeTrackingMultiple {
            get {
                if (this._timeTrackingMultiple == null)
                {
                    this._timeTrackingMultiple = new WorkFlowTimeTrackingMultiple(Session)
                    {
                        Workflow = this.View.CurrentObject as WorkflowBase
                    };
                }

                return this._timeTrackingMultiple;
            }
        }

        private Session _session;
        private Session Session {
            get {
                this._session = ((XPObjectSpace)CurrentObjectSpace).Session;
                return this._session;
            }
        }

        private IObjectSpace _currentObjectSpace;
        private IObjectSpace CurrentObjectSpace {
            get {
                this._currentObjectSpace = this.View.ObjectSpace;
                return this._currentObjectSpace;
            }
        }
        #endregion

        public PopupTimeTrackingController()
        {
            this.InitializeComponent();
            this.RegisterActions(this._components);
        }

        private void InitializeComponent()
        {
            this._components = new System.ComponentModel.Container();
            this._showTimeTracking = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this._components)
            {
                AcceptButtonCaption = "Save",
                ImageName = "Action_ShowHideDateNavigator",
                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image,
                Caption = "Time",
                Category = "Edit",
                ConfirmationMessage = null,
                Id = "TimeTracingAction",
                TargetObjectsCriteria = "Not IsNewObject(this)",
                ToolTip = "Capeture Time tracking for Employee's"
            };

            this._showTimeTracking.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ShowTimeTracking_CustomizePopupWindowParams);
            this._showTimeTracking.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ShowTimeTracking_Execute);
        }

        private void ShowTimeTracking_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.DialogController.SaveOnAccept = false;

            DetailView dv = Application.CreateDetailView(this.CurrentObjectSpace, this.TimeTrackingMultiple, false);
            dv.ViewEditMode = ViewEditMode.Edit;

            e.View = dv;
            e.View.Caption = "Time Tracking";
        }

        private void ShowTimeTracking_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Validator.RuleSet.Validate(View.ObjectSpace, this.TimeTrackingMultiple, "Immediate");

            this.TimeTrackingMultiple.Save();
            WorkFlowTimeTrackingMultipleHelper.Execute(this.TimeTrackingMultiple);

            this.Session.CommitTransaction();
            this._timeTrackingMultiple = null;

            ToastMessageHelper.ShowSuccessMessage(this.Application, "Times successfully addeed and saved", InformationPosition.Bottom);
        }
    }
}

