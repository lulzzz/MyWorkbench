using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.BusinessObjects.Communication.Common;
using MyWorkbench.BusinessObjects.Common;
using MyWorkbench.BusinessObjects.WorkFlow;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Data.Filtering;
using MyWorkbench.BusinessReports;
using Ignyt.BusinessInterface;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using MyWorkbench.Module.ModelGenerators;

namespace MyWorkbench.Module
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed partial class MyWorkbenchModule : ModuleBase
    {
        private ITypesInfo _typesInfo;

        public MyWorkbenchModule()
        {
            InitializeComponent();

            this.AdditionalExportedTypes.Add(typeof(OidGenerator));
            this.AdditionalExportedTypes.Add(typeof(JobCard));
            this.AdditionalExportedTypes.Add(typeof(Ticket));
            this.AdditionalExportedTypes.Add(typeof(Quote));
            this.AdditionalExportedTypes.Add(typeof(Invoice));
            this.AdditionalExportedTypes.Add(typeof(CreditNote));
            this.AdditionalExportedTypes.Add(typeof(Purchase));
            this.AdditionalExportedTypes.Add(typeof(RequestForQuote));
            this.AdditionalExportedTypes.Add(typeof(Settings));
            this.AdditionalExportedTypes.Add(typeof(SettingsPrefix));
            this.AdditionalExportedTypes.Add(typeof(Item));
            this.AdditionalExportedTypes.Add(typeof(Project));
            this.AdditionalExportedTypes.Add(typeof(WorkflowBase));
            this.AdditionalExportedTypes.Add(typeof(WorkFlowNote));
            this.AdditionalExportedTypes.Add(typeof(Employee));
            this.AdditionalExportedTypes.Add(typeof(Status));
            this.AdditionalExportedTypes.Add(typeof(Equipment));
            this.AdditionalExportedTypes.Add(typeof(Make));
            this.AdditionalExportedTypes.Add(typeof(Model));
            this.AdditionalExportedTypes.Add(typeof(Vendor));
            this.AdditionalExportedTypes.Add(typeof(CustomField));
            this.AdditionalExportedTypes.Add(typeof(RuleRequiredFieldPersistent));
            this.AdditionalExportedTypes.Add(typeof(Email));
            this.AdditionalExportedTypes.Add(typeof(Message));
            this.AdditionalExportedTypes.Add(typeof(EmailAddress));
            this.AdditionalExportedTypes.Add(typeof(CellNumber));
            this.AdditionalExportedTypes.Add(typeof(Sale));
            this.AdditionalExportedTypes.Add(typeof(Checklist));
            this.AdditionalExportedTypes.Add(typeof(Culture));
            this.AdditionalExportedTypes.Add(typeof(WorkFlow));
            this.AdditionalExportedTypes.Add(typeof(RecurringJobCard));
            this.AdditionalExportedTypes.Add(typeof(RecurringInvoice));
            this.AdditionalExportedTypes.Add(typeof(CommunicationLog));
            this.AdditionalExportedTypes.Add(typeof(ModelDifference));
            this.AdditionalExportedTypes.Add(typeof(MultiplePaymentList));
            this.AdditionalExportedTypes.Add(typeof(MultiplePayment));
            this.AdditionalExportedTypes.Add(typeof(SupplierInvoice));
            this.AdditionalExportedTypes.Add(typeof(WebhooksEvents));
            this.AdditionalExportedTypes.Add(typeof(AccountCategory));
            this.AdditionalExportedTypes.Add(typeof(VATType));
            this.AdditionalExportedTypes.Add(typeof(SupplierCreditNote));
            this.AdditionalExportedTypes.Add(typeof(Booking));
            this.AdditionalExportedTypes.Add(typeof(MyWorkbench.BusinessObjects.Office.Document));
            this.AdditionalExportedTypes.Add(typeof(MyWorkbench.BusinessObjects.Office.Spreadsheet));
            this.AdditionalExportedTypes.Add(typeof(MyWorkbench.BusinessObjects.Task));
            this.AdditionalExportedTypes.Add(typeof(WorkFlowProcess));
            this.AdditionalExportedTypes.Add(typeof(WorkFlowProcessTracking));
            this.AdditionalExportedTypes.Add(typeof(Account));
            this.AdditionalExportedTypes.Add(typeof(Banking));
            this.AdditionalExportedTypes.Add(typeof(RecurringTask));
            this.AdditionalExportedTypes.Add(typeof(RecurringBooking));

            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            PredefinedReportsUpdater predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);

            #region Predefined/Custom Reports
            ReportDataV2 reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(Invoice)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<InvoiceReport>(reportDataV2.DisplayName, typeof(Invoice), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<InvoiceReport>("Print Invoice", typeof(Invoice), isInplaceReport: true);

            reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(Quote)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<QuoteReport>(reportDataV2.DisplayName, typeof(Quote), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<QuoteReport>("Print Quote", typeof(Quote), isInplaceReport: true);

            reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(CreditNote)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<CreditNoteReport>(reportDataV2.DisplayName, typeof(CreditNote), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<CreditNoteReport>("Print Credit Note", typeof(CreditNote), isInplaceReport: true);

            reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(SupplierCreditNote)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<SupplierCreditNoteReport>(reportDataV2.DisplayName, typeof(SupplierCreditNote), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<SupplierCreditNoteReport>("Print Supplier Credit Note", typeof(SupplierCreditNote), isInplaceReport: true);

            reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(JobCard)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<JobCardReport>(reportDataV2.DisplayName, typeof(JobCard), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<JobCardReport>("Print Jobcard", typeof(JobCard), isInplaceReport: true);

            reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(Purchase)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<PurchaseReport>(reportDataV2.DisplayName, typeof(Purchase), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<PurchaseReport>("Print Purchase", typeof(Purchase), isInplaceReport: true);

            reportDataV2 = objectSpace.FindObject<ReportDataV2>(new BinaryOperator("PredefinedReportType", typeof(RequestForQuote)));

            if (reportDataV2 != null)
                predefinedReportsUpdater.AddPredefinedReport<RequestForQuoteReport>(reportDataV2.DisplayName, typeof(RequestForQuote), isInplaceReport: reportDataV2.IsInplaceReport);
            else
                predefinedReportsUpdater.AddPredefinedReport<RequestForQuoteReport>("Print Request For Quote", typeof(RequestForQuote), isInplaceReport: true);
            #endregion

            predefinedReportsUpdater.AddPredefinedReport<ClientStatementReport>("Print Statement", typeof(Vendor), isInplaceReport: true);
            predefinedReportsUpdater.AddPredefinedReport<TimeTrackingReport>("Time Tracking", typeof(WorkFlowTimeTracking), isInplaceReport: false);
            predefinedReportsUpdater.AddPredefinedReport<PickingSlipReport>("Picking Slip", typeof(MassInventoryMovement), isInplaceReport: true);
            predefinedReportsUpdater.AddPredefinedReport<TransactionReport>("Accounts Aging - Accounts", typeof(Transaction), isInplaceReport: false);
            predefinedReportsUpdater.AddPredefinedReport<TransactionReportCreditors>("Accounts Aging - Suppliers", typeof(Transaction), isInplaceReport: false);

            //CustomFieldHelper.CreateCustomFields(_typesInfo, objectSpace);

            return new ModuleUpdater[] { updater, predefinedReportsUpdater };
        }

        public override void Setup(XafApplication application)
        {
            base.Setup(application);

            application.CustomProcessShortcut += Application_CustomProcessShortcut;
        }

        private void Application_CustomProcessShortcut(object sender, CustomProcessShortcutEventArgs e)
        {
            if (Application.Model.Views[e.Shortcut.ViewId] is IModelListView view)
            {
                Type t = XafTypesInfo.Instance.FindTypeInfo(view.AsObjectView.ModelClass.Name).Type;
                if (t.GetInterface(typeof(ISingletonBO).FullName) != null)
                {
                    IObjectSpace space = Application.CreateObjectSpace(t);
                    object obj = space.FindObject(t, null, true);
                    if (obj == null)
                        obj = space.CreateObject(t);

                    e.View = Application.CreateDetailView(space, obj);
                    e.Handled = true;
                }
            }
        }

        public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters)
        {
            base.AddGeneratorUpdaters(updaters);

            updaters.Add(new ISingletonBOModelUpdater());
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
            _typesInfo = typesInfo;
        }
    }
}
