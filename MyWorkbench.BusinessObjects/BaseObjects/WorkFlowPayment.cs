using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.BaseObjects
{
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkFlowPayment : BaseObject, ITransactionType, IAccountingPartner, IModal
    {
        public WorkFlowPayment(Session session)
            : base(session)
        {
        }

        #region ITransactionType
        private Transaction fTransaction;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonCloneable]
        public Transaction Transaction {
            get {
                return fTransaction;
            }
            set {
                if (fTransaction == value)
                    return;

                Transaction prevTransaction = fTransaction;
                fTransaction = value;

                if (IsLoading) return;

                if (prevTransaction != null && prevTransaction.Payment == this)
                    prevTransaction.Payment = null;

                if (fTransaction != null)
                    fTransaction.Payment = this;

                OnChanged("Transaction");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public TransactionType TransactionType {
            get {
                if(this.Workflow == null)
                    return TransactionType.Payment;

                if (this.Workflow.WorkFlowType == WorkFlowType.Purchase)
                    return TransactionType.PurchasePayment;
                else if (this.Workflow.WorkFlowType == WorkFlowType.SupplierInvoice)
                    return TransactionType.SupplierPayment;
                else
                    return TransactionType.Payment;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime TransactionDate {
            get {
                return this.PaymentDate;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Amount {
            get {
                return Math.Round(this.PaymentAmount, 2);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IVendor IVendor {
            get {
                return this.Vendor == null ? this.Workflow != null ? this.Workflow.Vendor : null : this.Vendor;
            }
            set {
                this.Vendor = value as Vendor;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IWorkflow IWorkflow {
            get {
                return this.Workflow;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public ITransaction ITransaction {
            get {
                return this.Transaction;
            }
            set {
                this.Transaction = value as Transaction;
            }
        }
        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AdditionalDescription {
            get {
                return string.Empty;
            }
        }
        #endregion

        #region Properties
        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkFlowPayment")]
        [VisibleInDetailView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("Vendor.WorkflowBases")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private Vendor fVendor;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField("Vendor is required", DefaultContexts.Save, "Vendor is required if Workflow is empty", TargetCriteria = "Workflow = null")]
        [ImmediatePostData]
        [Appearance("Vendor", Visibility = ViewItemVisibility.Hide, Criteria = "Workflow != null", Context = "DetailView")]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);
            }
        }

        private DateTime fPaymentDate;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime PaymentDate {
            get {
                return fPaymentDate;
            }
            set {
                SetPropertyValue("PaymentDate", ref fPaymentDate, value);
            }
        }

        private double fPaymentAmount;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public double PaymentAmount {
            get {
                return Math.Round(fPaymentAmount, 2);
            }
            set {
                SetPropertyValue("PaymentAmount", ref fPaymentAmount, Math.Round(value, 2));
            }
        }

        private PaymentType fPaymentType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public PaymentType PaymentType {
            get {
                return fPaymentType;
            }
            set {
                SetPropertyValue("PaymentType", ref fPaymentType, value);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public double UnallocatedPayments {
            get {
                return 0;
            }
        }

        [VisibleInListView(false),VisibleInDetailView(false)]
        public string Description {
            get {
                return string.Format("{0} - {1}", Workflow != null ? this.Workflow.Vendor.FullName : null,PaymentType.ToString());
            }
        }
        #endregion

        private string fAccountingPartnerIdentifier;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AccountingPartnerIdentifier {
            get => fAccountingPartnerIdentifier;
            set => SetPropertyValue(nameof(AccountingPartnerIdentifier), ref fAccountingPartnerIdentifier, value);
        }

        #region Events
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                this.PaymentType = PaymentType.EFT;
                this.PaymentDate = Constants.DateTimeTimeZone(this.Session);
            }
        }

        protected override void OnSaved()
        {
            if (!this.IsDeleted)
            {
                TransactionTypeHelper.Instance.SaveTransaction(this);
            }
            else
            {
                TransactionTypeHelper.Instance.DeleteTransaction(this);
            }

            base.OnSaving();
        }
        #endregion
    }
}
