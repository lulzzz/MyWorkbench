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
    public class PopupEmailController : ObjectViewController<DetailView, IEmailPopup>
    {
        private System.ComponentModel.IContainer _components = null;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction _showEmailAction;
        private DetailView dvDetailView;

        #region Properties
        private Email _emailParameter;
        private Email EmailParameter {
            get {
                if (this._emailParameter == null) {
                    this._emailParameter = new Email(Session)
                    {
                        CurrentObject = Session.FindObject(this.View.CurrentObject.GetType(), CriteriaOperator.Parse("Oid == ?", (this.View.CurrentObject as BaseObject).Oid)) as IEmailPopup
                    };
                }

                return this._emailParameter;
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

        public PopupEmailController()
        {
            this.InitializeComponent();
            this.RegisterActions(this._components);
        }

        private void InitializeComponent()
        {
            this._components = new System.ComponentModel.Container();
            this._showEmailAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this._components)
            {
                AcceptButtonCaption = "Send",
                ImageName = "MailMerge",
                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image,
                Caption = "Email",
                Category = "Edit",
                Id = "EmailAction",
                TargetObjectsCriteria = "Not IsNewObject(this)",
                ToolTip = "Send Email"
            };

            this._showEmailAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ShowEmailAction_CustomizePopupWindowParams);
            this._showEmailAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ShowEmailAction_Execute);
            this._showEmailAction.Cancel += _showEmailAction_Cancel;
        }

        private void _showEmailAction_Cancel(object sender, System.EventArgs e)
        {
            this.Session.Delete(this._emailParameter);
            this.Session.CommitTransaction();

            ToastMessageHelper.ShowWarningMessage(this.Application, string.Concat("Email Message Cancelled"), InformationPosition.Bottom);

            this._emailParameter = null;
            this.dvDetailView = null;
        }

        private void ShowEmailAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.DialogController.SaveOnAccept = false;

            this.dvDetailView = Application.CreateDetailView(this.CurrentObjectSpace, this.EmailParameter, false);
            this.dvDetailView.ViewEditMode = ViewEditMode.Edit;

            e.View = this.dvDetailView;
            e.View.Caption = "Email";
        }

        private void ShowEmailAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Validator.RuleSet.Validate(View.ObjectSpace, this._emailParameter, "Immediate");

            this.EmailParameter.CommunicationLog.Add(new CommunicationLog(this.EmailParameter.Session) { Description = "Email Queued" });

            this.EmailParameter.Save();
            this.Session.CommitTransaction();

            ToastMessageHelper.ShowSuccessMessage(this.Application, string.Concat("Email Message ", this.EmailParameter.Status.ToString()), InformationPosition.Bottom);

            this._emailParameter = null;
            this.dvDetailView = null;
        }
    }
}

