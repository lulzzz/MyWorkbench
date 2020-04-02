using DevExpress.ExpressApp;
using System;
using MyWorkbench.BusinessObjects.Accounts;
using Ignyt.BusinessInterface;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using System.Drawing;
using DevExpress.ExpressApp.Actions;
using MyWorkbench.BusinessObjects.BaseObjects;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;

namespace MyWorkbench.Module.Web.Controllers
{
    public class PaymentController : ObjectViewController<ListView, IMultiplePayments>
    {
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ShowPaymentAction;
        private System.ComponentModel.IContainer components = null;
        private DetailView dvDetailView;

        private MultiplePaymentList multiplePaymentList;
        private MultiplePaymentList MultiplePaymentList {
            get {
                return multiplePaymentList;
            }
            set {
                multiplePaymentList = value;
            }
        }

        private IObjectSpace _currentObjectSpace;
        private IObjectSpace CurrentObjectSpace {
            get {
                this._currentObjectSpace = this.View.ObjectSpace;
                return this._currentObjectSpace;
            }
        }

        private Session _session;
        private Session Session {
            get {
                this._session = ((XPObjectSpace)CurrentObjectSpace).Session;
                return this._session;
            }
        }

        public PaymentController()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.ShowPaymentAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components)
            {
                AcceptButtonCaption = null,
                CancelButtonCaption = null,
                Caption = "Enter Payment",
                ConfirmationMessage = null,
                Id = "MultiplePayment",
                ImageName = "BO_Sale_Item",
                ToolTip = "Capture multiple payments",
                TypeOfView = typeof(DevExpress.ExpressApp.ListView),
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
            };

            this.ShowPaymentAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ShowPaymentAction_CustomizePopupWindowParams);
            this.ShowPaymentAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ShowPaymentAction_Execute);
            this.Actions.Add(this.ShowPaymentAction);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (((ListView)this.View).Editor is ASPxGridListEditor listEditor)
                listEditor.Grid.Styles.AlternatingRow.BackColor = Color.FromArgb(244, 244, 244);
        }

        private void ShowPaymentAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            this.LoadMultiplePaymentList();

            e.DialogController.SaveOnAccept = false;

            this.dvDetailView = Application.CreateDetailView(this.CurrentObjectSpace, this.MultiplePaymentList, false);
            this.dvDetailView.ViewEditMode = ViewEditMode.Edit;

            e.View = this.dvDetailView;
            e.View.Caption = "Multiple Payments";
        }

        private void ShowPaymentAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            this.SavePayments();
            this.CurrentObjectSpace.CommitChanges();
            this.ObjectSpace.Refresh();
        }

        private void LoadMultiplePaymentList()
        {
            this.MultiplePaymentList = new MultiplePaymentList(this.Session);
            foreach (WorkflowBase workFlowBase in View.SelectedObjects)
            {
                MultiplePaymentList.MultiplePayments.Add(new MultiplePayment(this.Session)
                {
                    Workflow = workFlowBase,
                    PaymentAmount = workFlowBase.AmountOutstanding,
                    PaymentDate = DateTime.Today,
                    PaymentType = PaymentType.EFT,
                    Vendor = workFlowBase.Vendor
                });
            }
        }

        private void SavePayments()
        {
            foreach (MultiplePayment multiplePayment in multiplePaymentList.MultiplePayments)
            {
                WorkFlowPayment payment = this.CurrentObjectSpace.CreateObject<WorkFlowPayment>();

                payment.Workflow = CurrentObjectSpace.GetObject(multiplePayment.Workflow);
                payment.PaymentAmount = multiplePayment.PaymentAmount;
                payment.PaymentDate = multiplePayment.PaymentDate;
                payment.PaymentType = multiplePayment.PaymentType;

                payment.Save();
            }
        }
    }
}
