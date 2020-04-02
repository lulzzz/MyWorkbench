//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.Xpo;
//using DevExpress.Persistent.Base;
//using DevExpress.Xpo;
//using MyWorkbench.BusinessObjects.Accounts;
//using MyWorkbench.BusinessObjects.Helpers;
//using MyWorkbench.Module.Web.Helpers;
//using System;
//using System.Collections.Generic;

//namespace MyWorkbench.Module.Web.Controllers
//{
//    public class ExportInvoicesController : ObjectViewController<ListView,Invoice>
//    {
//        #region Properties
//        private IObjectSpace _currentObjectSpace;
//        private IObjectSpace CurrentObjectSpace {
//            get {
//                this._currentObjectSpace = this.View.ObjectSpace;
//                return this._currentObjectSpace;
//            }
//        }

//        private Session _session;
//        private Session Session {
//            get {
//                this._session = ((XPObjectSpace)CurrentObjectSpace).Session;
//                return this._session;
//            }
//        }

//        private AccountingPackageExport _accountingPackageExport;
//        private AccountingPackageExport AccountingPackageExport {
//            get {
//                if (_accountingPackageExport == null)
//                {
//                    if (MyWorkbench.BaseObjects.Constants.Constants.AccountingPartner(this.Session) == Ignyt.BusinessInterface.AccountingPartner.XeroAccounting)
//                        _accountingPackageExport = new XeroAccountingExport(this.Session);
//                    else
//                        _accountingPackageExport = new SageOneAccountingExport(this.Session);
//                }
//                return _accountingPackageExport;
//            }
//        }
//        #endregion

//        public ExportInvoicesController()
//        {
//            var action = new SimpleAction(this, "ExportInvoices", PredefinedCategory.Tools)
//            {
//                SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent,
//                TargetObjectsCriteria = "[Oid] Is Not Null",
//                ConfirmationMessage = "Are you sure you would like to export the selected invoices to your accounting package?",
//                ToolTip = "Export invoices to your Accounting Package",
//                ImageName = "Action_Export",
//                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image,
//                Caption = "Export",
//                Category = "Edit"
//            };

//            action.Execute += Action_Execute;
//        }


//        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
//        {
//            List<Invoice> invoices = new List<Invoice>();

//            try
//            {
//                foreach (Invoice invoice in this.View.SelectedObjects)
//                {
//                    invoices.Add(invoice);
//                }

//                AccountingPackageExport.ExportInvoices(invoices);
//                AccountingPackageExport.Commit();

//                ToastMessageHelper.ShowSuccessMessage(this.Application, "Export successfully executed. Please view results for details", InformationPosition.Bottom);
//            }
//            catch (Exception ex)
//            {
//                ToastMessageHelper.ShowErrorMessage(this.Application, ex, InformationPosition.Bottom);
//            }
//        }
//    }

//    public class ExportInvoiceController : ObjectViewController<DetailView, Invoice>
//    {
//        #region Properties
//        private IObjectSpace _currentObjectSpace;
//        private IObjectSpace CurrentObjectSpace {
//            get {
//                this._currentObjectSpace = this.View.ObjectSpace;
//                return this._currentObjectSpace;
//            }
//        }

//        private Session _session;
//        private Session Session {
//            get {
//                this._session = ((XPObjectSpace)CurrentObjectSpace).Session;
//                return this._session;
//            }
//        }

//        private AccountingPackageExport _accountingPackageExport;
//        private AccountingPackageExport AccountingPackageExport {
//            get {
//                if (_accountingPackageExport == null)
//                {
//                    if (MyWorkbench.BaseObjects.Constants.Constants.AccountingPartner(this.Session) == Ignyt.BusinessInterface.AccountingPartner.XeroAccounting)
//                        _accountingPackageExport = new XeroAccountingExport(this.Session);
//                    else
//                        _accountingPackageExport = new SageOneAccountingExport(this.Session);
//                }
//                return _accountingPackageExport;
//            }
//        }
//        #endregion

//        public ExportInvoiceController()
//        {
//            var action = new SimpleAction(this, "ExportInvoice", PredefinedCategory.Tools)
//            {
//                SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent,
//                TargetObjectsCriteria = "[Oid] Is Not Null",
//                ConfirmationMessage = "Are you sure you would like to export the selected invoice to your accounting package?",
//                ToolTip = "Export invoice to your Accounting Package",
//                ImageName = "Action_Export",
//                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image,
//                Caption = "Export",
//                Category = "Edit"
//            };

//            action.Execute += Action_Execute;
//        }


//        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
//        {
//            try
//            {
//                AccountingPackageExport.ExportInvoice(this.View.CurrentObject as Invoice);
//                AccountingPackageExport.Commit();
//                View.ObjectSpace.ReloadObject(this.View.CurrentObject);

//                ToastMessageHelper.ShowSuccessMessage(this.Application, "Export successfully executed. Please view results for details", InformationPosition.Bottom);
//            }
//            catch (Exception ex)
//            {
//                ToastMessageHelper.ShowErrorMessage(this.Application, ex, InformationPosition.Bottom);
//            }
//            finally
//            {

//                View.ObjectSpace.ReloadObject(View.CurrentObject);
//            }
//        }
//    }
//}
