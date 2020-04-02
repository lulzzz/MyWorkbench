using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.Module.Web.Helpers;

namespace Diggit.Framework.Controllers {
    public class PopupMessageController : ObjectViewController<DetailView, IMessagePopup>
    {
        private System.ComponentModel.IContainer _components = null;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction _showMessageAction;
        private DetailView dvDetailView;

        #region Properties
        private Message _messageParameter;
        private Message MessageParameter {
            get {
                if (this._messageParameter == null)
                {
                    this._messageParameter = new Message(Session)
                    {
                        CurrentObject = Session.FindObject(this.View.CurrentObject.GetType(), CriteriaOperator.Parse("Oid == ?", (this.View.CurrentObject as BaseObject).Oid)) as IMessagePopup
                    };
                }

                return this._messageParameter;
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

        public PopupMessageController()
        {
            this.InitializeComponent();
            this.RegisterActions(this._components);
        }

        private void InitializeComponent()
        {
            this._components = new System.ComponentModel.Container();
            this._showMessageAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this._components)
            {
                AcceptButtonCaption = "Send",
                ImageName = "BO_Phone",
                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image,
                Caption = "Message",
                Category = "Edit",
                Id = "MessageAction",
                TargetObjectsCriteria = "Not IsNewObject(this)",
                ToolTip = "Send Message"
            };

            this._showMessageAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ShowEmailAction_CustomizePopupWindowParams);
            this._showMessageAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ShowEmailAction_Execute);
            this._showMessageAction.Cancel += _showMessageAction_Cancel;
        }

        private void _showMessageAction_Cancel(object sender, System.EventArgs e)
        {
            this.Session.Delete(this._messageParameter);
            this.Session.CommitTransaction();

            ToastMessageHelper.ShowWarningMessage(this.Application, string.Concat("Message Cancelled"), InformationPosition.Bottom);

            this._messageParameter = null;
            this.dvDetailView = null;
        }

        private void ShowEmailAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.DialogController.SaveOnAccept = false;

            this.dvDetailView = Application.CreateDetailView(this.CurrentObjectSpace, this.MessageParameter, false);
            this.dvDetailView.ViewEditMode = ViewEditMode.Edit;

            e.View = this.dvDetailView;
            e.View.Caption = "Message";
        }

        private void ShowEmailAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Validator.RuleSet.Validate(View.ObjectSpace, this._messageParameter, "Immediate");

            this.MessageParameter.CommunicationLog.Add(new CommunicationLog(this.MessageParameter.Session) { Description = "Message Queued" });

            this.MessageParameter.Save();
            this.Session.CommitTransaction();

            ToastMessageHelper.ShowSuccessMessage(this.Application, string.Concat("Message ", this.MessageParameter.Status.ToString()), InformationPosition.Bottom);

            this._messageParameter = null;
            this.dvDetailView = null;
        }
    }
}
